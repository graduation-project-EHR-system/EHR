using EHR.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Dtos
{
    public class HospitalDto
    {

        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Director Name is required.")]
        [MaxLength(255)]
        public string DirectorName { get; set; }

        [Required(ErrorMessage = "Hospital Code is required.")]
        [MaxLength(255)]
        public string HospitalCode { get; set; }

        [Required(ErrorMessage = "Licences Number is required.")]
        [MaxLength(255)]
        public string LicencesNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(255)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Type is required.")]
        [EnumDataType(typeof(Entities.Type), ErrorMessage = "Invalid Type.")]
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
