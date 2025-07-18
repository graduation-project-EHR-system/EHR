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
    public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
    {
        public void Configure(EntityTypeBuilder<Observation> builder)
        {
            builder.ToTable("Observations");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.TestName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(prop => prop.Value)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(prop => prop.Unit)
                .HasMaxLength(20);

            builder.Property(prop => prop.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
