using EHR.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Core.ServicesContract
{
    public interface IMedicalRecordService
    {
        public Task<MedicalRecordDto> CreateMedicalRecordAsync(MedicalRecordDto medicalRecordsDto);

        public Task<PaginationData> GetAllMedicalRecordsAsync(PaginationRequest paginationRequest);

        public Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid id);
        public Task<PaginationData> GetAllMedicalRecordForPatientByIdAsync(Guid id, PaginationRequest paginationRequest);
        public Task<UpdateMedicalRecordDto> UpdateMedicalRecordAsync(UpdateMedicalRecordDto medicalRecordsDto);
    }
}
