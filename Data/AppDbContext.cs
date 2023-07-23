using ContactListApi.Data.Mappings;
using ContactListApi.Enums;
using ContactListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListApi.Data
{
    public class AppDbContext : DbContext
    {
        public string DbPath { get; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        public AppDbContext()
        {
            var folder = System.IO.Directory.GetCurrentDirectory();
            DbPath = System.IO.Path.Join(folder, @"Data\contactListApi.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactTypeMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new PersonMap());

            modelBuilder.Entity<ContactType>().HasData(
                new ContactType { Id = Convert.ToInt32(ContactTypeEnum.Phone), Name = "Telefone" },
                new ContactType { Id = Convert.ToInt32(ContactTypeEnum.Email), Name = "Email" },
                new ContactType { Id = Convert.ToInt32(ContactTypeEnum.WhatsApp), Name = "WhatsApp" }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath};Cache=Shared");
    }
}