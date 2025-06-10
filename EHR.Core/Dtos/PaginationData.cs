using MedicalRecords.Service.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Dtos
{
    public class PaginationData
    {
        public PaginationResponseWithData Meta { get; set; }
        public IEnumerable<MedicalRecordDto> Items { get; set; } = new List<MedicalRecordDto>();
    }
}
