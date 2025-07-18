﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Dtos
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(500, ErrorMessage = "Diagnosis must not exceed 500 characters.")]
        public string Diagnosis { get; set; }

        [StringLength(1000, ErrorMessage = "Notes must not exceed 1000 characters.")]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "Hospital ID is required.")]
        public Guid HospitalId { get; set; }

        [Required(ErrorMessage = "Doctor ID is required.")]
        public Guid CachedDoctorId { get; set; }

        public ICollection<MedicationDto> Medications { get; set; } = new List<MedicationDto>();
        public ICollection<ConditionDto> Conditions { get; set; } = new List<ConditionDto>();
        public ICollection<ObservationDto> Observations { get; set; } = new List<ObservationDto>();
    }
}
