using ManagingContent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagingContent.Db
{
    public class AppDbContext:DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=HAKEEM-DESKTOP\\SQLEXPRESS;Initial Catalog=Content;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }
        public AppDbContext() { }


    }
}
