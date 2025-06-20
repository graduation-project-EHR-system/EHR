﻿using EHR.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Configurations
{
    public class MedicationConfiguratoin : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("Medications");

            builder.HasKey(prop => prop.Id);

            builder.Property(builder => builder.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(builder => builder.Dosage)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(builder => builder.Frequency)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(builder => builder.DurationInDays)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
