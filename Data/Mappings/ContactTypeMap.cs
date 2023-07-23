using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContactListApi.Models;

namespace ContactListApi.Data.Mappings
{
    public class ContactTypeMap : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            builder.ToTable("ContactType");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("INTEGER");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("TEXT")
                .HasMaxLength(20);
        }
    }
}