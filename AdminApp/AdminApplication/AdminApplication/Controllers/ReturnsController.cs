using AdminApplication.Models;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System.Text;

namespace AdminApplication.Controllers
{
    public class ReturnsController : Controller
    {
        public ReturnsController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult ReturnsHome() 
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage=client.GetAsync("https://localhost:44331/api/Admin/getReturns").Result;

            var data = responseMessage.Content.ReadAsAsync<List<Returns>>().Result;

            return View(data);
        }

        public IActionResult getReturn(Guid id)
        {
            HttpClient client=new HttpClient();

            var model = new
            {
                Id = id
            };

            HttpContent content=new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync("https://localhost:44331/api/Admin/GetReturn", content).Result;

            var data=responseMessage.Content.ReadAsAsync<Returns>().Result;

            return View(data);
        }



        public FileContentResult ExportToPDF(Guid id)
        {
            HttpClient client = new HttpClient();

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync("https://localhost:44331/api/Admin/GetReturn", content).Result;

            var data = responseMessage.Content.ReadAsAsync<Returns>().Result;


            var TempPath = Path.Combine(Directory.GetCurrentDirectory(), "RentalPDF.docx");

            var document=DocumentModel.Load(TempPath);

            StringBuilder stringBuilder=new StringBuilder();

            
            document.Content.Replace("{{Customer}}",data.CustomerName);
            document.Content.Replace("{{DateFrom}}", data.Rental.Date_from.ToString());
            document.Content.Replace("{{DateTo}}", data.Rental.Date_on.ToString());
            document.Content.Replace("{{TotalDays}}", (data.Rental.Date_on.DayNumber - data.Rental.Date_from.DayNumber).ToString());
            document.Content.Replace("{{CarModel}}",data.Rental.car.Brand.Manufacturer+" "+data.Rental.car.Brand.Model);
            document.Content.Replace("{{FullPrice}}",data.Rental.FullPrice.ToString()+" $");
            document.Content.Replace("{{Add}}", data.Additional_fee.ToString()+" $");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(),new PdfSaveOptions().ContentType,"Rental-"+data.Id.ToString().Substring(30)+".pdf");
        }


        public FileContentResult ExportAllToPDF()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = client.GetAsync("https://localhost:44331/api/Admin/getReturns").Result;

            var data = responseMessage.Content.ReadAsAsync<List<Returns>>().Result;

            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            
            foreach ( var item in data)
            {
                stringBuilder.AppendLine(item.Id.ToString());
                stringBuilder1.AppendLine(item.CustomerName);
            }

            var TempPath = Path.Combine(Directory.GetCurrentDirectory(), "ALLRentalsPDF.docx");

            var document = DocumentModel.Load(TempPath);

            document.Content.Replace("{{ID}}",stringBuilder.ToString());
            document.Content.Replace("{{Customer}}",stringBuilder1.ToString());
            

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ALL-Rentals-" + data.Count().ToString() + ".pdf");
        }


        public FileContentResult exportToExcel(Guid id)
        {

            string fileName = "Order"+id.ToString().Substring(30)+".xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Orders");
                worksheet.Cell(1, 1).Value = "Rental ID";
                worksheet.Cell(1, 2).Value = "Customer UserName";
                worksheet.Cell(1, 3).Value = "Date From";
                worksheet.Cell(1, 4).Value = "Date To";
                worksheet.Cell(1, 5).Value = "Total Days";
                worksheet.Cell(1, 6).Value = "Car Model";
                worksheet.Cell(1, 7).Value = "Full Price";
                worksheet.Cell(1, 8).Value = "Additional Fee";

                HttpClient client = new HttpClient();

                var model = new
                {
                    Id = id
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = client.PostAsync("https://localhost:44331/api/Admin/GetReturn", content).Result;

                var data = responseMessage.Content.ReadAsAsync<Returns>().Result;

                worksheet.Cell(2, 1).Value = data.Id.ToString();
                worksheet.Cell(2, 2).Value = data.CustomerName;
                worksheet.Cell(2, 3).Value = data.Rental?.Date_from.ToString();
                worksheet.Cell(2, 4).Value = data.Rental?.Date_on.ToString();
                worksheet.Cell(2, 5).Value = data.Rental?.Date_on.DayNumber-data.Rental?.Date_from.DayNumber;
                worksheet.Cell(2, 6).Value = data.Rental?.car?.Brand.Manufacturer+" " + data.Rental?.car?.Brand.Model;
                worksheet.Cell(2, 7).Value = data.Rental?.FullPrice.ToString()+" $";
                worksheet.Cell(2, 8).Value = data.Additional_fee.ToString()+" $";


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var con = stream.ToArray();
                    return File(con, contentType, fileName);
                }
            }
        }


    }
}
