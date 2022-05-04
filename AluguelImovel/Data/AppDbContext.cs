using AluguelImovel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AluguelImovel
{
    public class AppDbContext : DbContext
    {
        public DbSet<Imovel> Imovel { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build();

            var connection = config.GetConnectionString("DefaultConnectionString");

            // define the database to use
            if (!string.IsNullOrEmpty(connection))
            {
                optionsBuilder.UseMySQL(connection);
                
                
            }
        }

    }
}
