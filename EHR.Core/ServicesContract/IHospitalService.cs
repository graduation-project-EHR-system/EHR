using EHR.Core.Dtos;
using EHR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.ServicesContract
{
    public interface IHospitalService
    {
        Task<PaginationHospital> GetHospitalsAsync(PaginationRequest paginationRequest);
        Task<HospitalDto> GetHospitalByIdAsync(Guid id);
        Task<HospitalDto> AddHospitalAsync(HospitalDto hospital);
        Task<HospitalDto> UpdateHospitalAsync(HospitalDto hospital);
        Task<bool> DeleteHospitalAsync(Guid id);
    }
}
