using AutoMapper;
using EHR.Core;
using EHR.Core.Dtos;
using EHR.Core.Entities;
using EHR.Core.ServicesContract;
using MedicalRecords.Service.Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Service.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly EHRdbContext _dbContext;
        private readonly IMapper _mapper;

        public HospitalService(EHRdbContext DbContext , IMapper mapper)
        {
            _dbContext = DbContext;
            _mapper = mapper;
        }
        public async Task<HospitalDto> AddHospitalAsync(HospitalDto hospitalDto)
        {
            var result = _mapper.Map<Hospital>(hospitalDto);

            if (result is not null)
            {
                _dbContext.Add(result);
                await _dbContext.SaveChangesAsync();
            }
            else
                return null;

            hospitalDto.Id = result.Id;

            return hospitalDto;
        }

        public async Task<bool> DeleteHospitalAsync(Guid id)
        {
            Hospital hospital = await _dbContext.Hospitals.FindAsync(id);

            if (hospital is null)
                return false;

            _dbContext.Hospitals.Remove(hospital);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<HospitalDto> GetHospitalByIdAsync(Guid id)
        {
            var hospital =await _dbContext.Hospitals
                .FirstOrDefaultAsync();

            if(hospital is null)
                return null;

            var hospitalDto = _mapper.Map<HospitalDto>(hospital);

            return hospitalDto;
        }

        public async Task<PaginationHospital> GetHospitalsAsync(PaginationRequest paginationRequest)
        {
            var query = _dbContext.Hospitals
                .OrderByDescending(prop => prop.CreatedAt);

            int totalRecords = await query.CountAsync();

            double last = totalRecords * 1.0 / paginationRequest.PageSize;

            int lastPage = Convert.ToInt32(Math.Ceiling(last));


            var records = await query.
                 Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(m => new HospitalDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    DirectorName = m.DirectorName,
                    HospitalCode = m.HospitalCode,
                    LicencesNumber = m.LicencesNumber,
                    Address = m.Address,
                    IsActive = m.IsActive,
                    Type = m.Type.ToString(),
                    CreatedAt = m.CreatedAt
                
                })
            .ToListAsync();

            if (records.Count == 0 && totalRecords > 0)
                return null;


            var meta = new PaginationResponseWithData
            {
                CurrentPage = paginationRequest.PageNumber,
                PerPage = paginationRequest.PageSize,
                LastPage = lastPage,
                Total = totalRecords
            };

            return new PaginationHospital
            {
                Items = records,
                Meta = meta
            };

        }

        

        public async Task<HospitalDto> UpdateHospitalAsync(HospitalDto hospitalDto)
        {
            Hospital hospital = await _dbContext.Hospitals.FindAsync(hospitalDto.Id);

            hospital.Name = hospitalDto.Name;
            hospital.DirectorName = hospitalDto.DirectorName;
            hospital.HospitalCode = hospitalDto.HospitalCode;
            hospital.LicencesNumber = hospitalDto.LicencesNumber;
            hospital.Address = hospitalDto.Address;
            hospital.IsActive = hospitalDto.IsActive;
            hospital.Type = (Core.Entities.Type)Enum.Parse(typeof(Core.Entities.Type), hospitalDto.Type);


            await _dbContext.SaveChangesAsync();

            return hospitalDto;
        }
    }
}
