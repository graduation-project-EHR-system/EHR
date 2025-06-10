using MedicalRecords.Service.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Dtos
{
    public class PaginationHospital
    {
        public PaginationResponseWithData Meta { get; set; }
        public IEnumerable<HospitalDto> Items { get; set; } = new List<HospitalDto>();
    }
}
