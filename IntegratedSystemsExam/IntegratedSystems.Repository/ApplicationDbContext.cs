using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.Identity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntegratedSystems.Repository
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public virtual DbSet<Returns> Returns { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        
    }
}
