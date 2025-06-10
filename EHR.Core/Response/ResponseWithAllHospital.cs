using EHR.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.Response
{
    public class ResponseWithAllHospital : ApiResponse
    {
        public PaginationHospital Data { get; set; }
        public ResponseWithAllHospital(int statusCode, PaginationHospital data, string? message = null) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
