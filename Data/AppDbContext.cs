using ContactListApi.Data.Mappings;
using ContactListApi.Enums;
using ContactListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

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
    }
}