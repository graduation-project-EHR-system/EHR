using EHR.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Response
{
    public class ResponseWithAllMedicalRecordData : ApiResponse
    {
        public PaginationData Data { get; set; }
        public ResponseWithAllMedicalRecordData(int statusCode, PaginationData data, string? message = null) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
