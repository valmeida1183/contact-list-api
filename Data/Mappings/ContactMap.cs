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
                .HasColumnName("Id")
                .HasColumnType("TEXT");

            builder.Property(x => x.Value)
               .IsRequired()
               .HasColumnName("Value")
               .HasColumnType("TEXT");

            builder.Property(x => x.PersonId)
                .IsRequired()
                .HasColumnName("PersonId")
                .HasColumnType("TEXT");

            builder.Property(x => x.ContactTypeId)
                .IsRequired()
                .HasColumnName("ContactTypeId")
                .HasColumnType("INTEGER");

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