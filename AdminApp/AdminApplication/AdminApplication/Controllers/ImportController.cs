using AdminApplication.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class ImportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ImportCars(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Car> cars = getAllCars(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44331/api/Admin/ImportAllCars";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(cars), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            return RedirectToAction("ReturnsHome", "Returns");
        }

        private List<Car> getAllCars(string fileName)
        {
            List<Car> cars = new List<Car>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        Car car = new Car();
                        car.ImgURL = reader.GetValue(5).ToString();
                        car.Brand=new CarBrand();
                        car.Brand.Model = reader.GetValue(0).ToString();
                        car.Brand.Manufacturer = reader.GetValue(1).ToString();
                        car.Brand.Km = Convert.ToInt32(reader.GetValue(2));
                        car.PricePerDay = Convert.ToInt32(reader.GetValue(4));
                        car.Brand.year=Convert.ToInt32(reader.GetValue(3));
                        
                        cars.Add(car);
                    }
                }
            }
            return cars;
        }
    }
}
