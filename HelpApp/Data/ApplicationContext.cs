using HelpApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Appointment_records> Appointment_records { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Doctors_recommendations> Doctors_recommendations { get; set; }
        public DbSet<Game_history> Game_history { get; set; }
        public DbSet<General_blood_analysis> General_blood_analysis { get; set; }
        public DbSet<General_urine_analysis> General_urine_analysis { get; set; }
        public DbSet<Hospitals> Hospitals { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<Passport> Passport { get; set; }
        public DbSet<Street> Street { get; set; }
        public DbSet<Surgeries_graphic> Surgeries_graphic { get; set; }
        public DbSet<Surgeries_history> Surgeries_history { get; set; }
        public DbSet<Surgery_reports> Surgery_reports { get; set; }
        public DbSet<Xray_results> Xray_results { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=tracker;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}
