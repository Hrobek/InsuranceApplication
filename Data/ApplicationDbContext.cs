using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InsureApp.Models;

namespace InsureApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<InsureApp.Models.Users> Insured { get; set; } = default!;
        public DbSet<InsureApp.Models.Insurances> Insurances { get; set; } = default!;
        public DbSet<InsureApp.Models.UsersInsurances> UsersInsurances { get; set; } = default!;
    }
}
