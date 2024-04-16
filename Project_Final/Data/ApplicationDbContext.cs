using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Project_Final.Models.Doctor> Doctor { get; set; } = default!;
        public DbSet<Project_Final.Models.Visits> Visits { get; set; } = default!;
        public DbSet<Project_Final.Models.Patient> Patient { get; set; } = default!;
        public DbSet<Project_Final.Models.Prescription> Prescription { get; set; } = default!;
        public DbSet<Project_Final.Models.Message> Message { get; set; } = default!;
        public DbSet<Project_Final.Models.Reply> Reply { get; set; } = default!;

    }
}
