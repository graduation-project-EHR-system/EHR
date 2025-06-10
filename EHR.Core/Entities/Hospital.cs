using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Entities
{
    public class Hospital
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string DirectorName { get; set; }
        public string HospitalCode { get; set; }
        public string LicencesNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public Type Type { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
