using EHR.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Configurations
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.ToTable("Hospitals");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(prop => prop.HospitalCode)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(prop => prop.LicencesNumber)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(prop => prop.Address)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(prop => prop.DirectorName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(prop => prop.Type)
                .HasConversion<string>()
                .HasColumnType("nvarchar(10)");



            builder.Property(prop => prop.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
