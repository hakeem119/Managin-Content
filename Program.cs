
using Microsoft.EntityFrameworkCore;
using ManagingContent.Models;
using Microsoft.AspNetCore.Identity;
using ManagingContent.Db;
using Microsoft.Extensions.FileProviders;

namespace ManagingContent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Add services to the container.

            var uploadsFolderPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            builder.Services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(uploadsFolderPath)
            );

            new AppDbContext().Database.EnsureDeleted();
            new AppDbContext().Database.EnsureCreated();
            

            //builder.Services.AddDbContext<AppDbContext>(options =>
            //   options.UseSqlServer(builder.Configuration.GetConnectionString("Cs")));
            //var connectionString = builder.Configuration.GetConnectionString("Cs");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsFolderPath),
                RequestPath = "/Uploads"
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
