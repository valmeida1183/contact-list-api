using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContactListApi.Models;

namespace ContactListApi.Data.Mappings
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.Property(x => x.Value)
               .IsRequired()
               .HasColumnName("Value")
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50);

            builder.Property(x => x.PersonId)
                .IsRequired()
                .HasColumnName("PersonId");


            builder.Property(x => x.ContactTypeId)
                .IsRequired()
                .HasColumnName("ContactTypeId")
                .HasColumnType("INT");

            builder.HasOne(x => x.Person)
                .WithMany(x => x.Contacts)
                .HasConstraintName("FK_Contact_Person")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Type)
                .WithMany(x => x.Contacts)
                .HasConstraintName("FK_Contact_Type")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}