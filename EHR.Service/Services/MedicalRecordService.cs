﻿using AutoMapper;
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
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly EHRdbContext _dbContext;
        private readonly IMapper _mapper;

        public MedicalRecordService(EHRdbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MedicalRecordDto> CreateMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
        {
 
            var result = _mapper.Map<MedicalRecord>(medicalRecordDto);

            if (result is not null)
            {
                _dbContext.Add(result);
                await _dbContext.SaveChangesAsync();
            }

            medicalRecordDto.Id = result.Id;

            return medicalRecordDto;
        }

        public async Task<PaginationData> GetAllMedicalRecordForPatientByIdAsync(Guid id, PaginationRequest paginationRequest)
        {
            var query = _dbContext.MedicalRecords
                .Include(prop => prop.Observations)
                .Include(prop => prop.Conditions)
                .Include(prop => prop.Medications)
                .OrderByDescending(prop => prop.CreatedAt)
                .Where(p => p.PatientId == id);

            int totalRecords = await query.CountAsync();

            double last = totalRecords * 1.0 / paginationRequest.PageSize;

            int lastPage = Convert.ToInt32(Math.Ceiling(last));


            var records = await query.
                 Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(m => new MedicalRecordDto
                {
                    Id = m.Id,
                    HospitalId = m.HospitalId,
                    Diagnosis = m.Diagnosis,
                    Notes = m.Notes,
                    CreatedAt = m.CreatedAt,
                    PatientId = m.PatientId,
                    Medications = m.Medications.Select(med => new MedicationDto
                    {
                        Name = med.Name,
                        Dosage = med.Dosage,
                        Frequency = med.Frequency,
                        DurationInDays = med.DurationInDays
                    }).ToList(),
                    Conditions = m.Conditions.Select(c => new ConditionDto
                    {
                        Code = c.Code,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt
                    }).ToList(),
                    Observations = m.Observations.Select(o => new ObservationDto
                    {
                        TestName = o.TestName,
                        Value = o.Value,
                        Unit = o.Unit,
                        CreatedAt = o.CreatedAt
                    }).ToList()
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

            return new PaginationData
            {
                Items = records,
                Meta = meta
            };
        }

        public async Task<PaginationData> GetAllMedicalRecordsAsync(PaginationRequest paginationRequest)
        {
            var query = _dbContext.MedicalRecords
                .Include(prop => prop.Observations)
                .Include(prop => prop.Conditions)
                .Include(prop => prop.Medications)
                .OrderByDescending(prop => prop.CreatedAt);

            int totalRecords = await query.CountAsync();

            double last = totalRecords * 1.0 / paginationRequest.PageSize;

            int lastPage = Convert.ToInt32(Math.Ceiling(last));


            var records = await query.
                 Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(m => new MedicalRecordDto
                {
                    Id = m.Id,
                    HospitalId = m.HospitalId,
                    Diagnosis = m.Diagnosis,
                    Notes = m.Notes,
                    CreatedAt = m.CreatedAt,
                    PatientId = m.PatientId,
                    Medications = m.Medications.Select(med => new MedicationDto
                    {
                        Name = med.Name,
                        Dosage = med.Dosage,
                        Frequency = med.Frequency,
                        DurationInDays = med.DurationInDays
                    }).ToList(),
                    Conditions = m.Conditions.Select(c => new ConditionDto
                    {
                        Code = c.Code,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt
                    }).ToList(),
                    Observations = m.Observations.Select(o => new ObservationDto
                    {
                        TestName = o.TestName,
                        Value = o.Value,
                        Unit = o.Unit,
                        CreatedAt = o.CreatedAt
                    }).ToList()
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

            return new PaginationData
            {
                Items = records,
                Meta = meta
            };
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid id)
        {
            MedicalRecord medicalRecord = await _dbContext.MedicalRecords
                                                .Include(prop => prop.Observations)
                                                .Include(prop => prop.Conditions)
                                                .Include(prop => prop.Medications)
                                                .FirstOrDefaultAsync(m => m.Id == id);


            if (medicalRecord is null)
                return null;

            MedicalRecordDto medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);

            return medicalRecordDto;
        }

        public async Task<UpdateMedicalRecordDto> UpdateMedicalRecordAsync(UpdateMedicalRecordDto medicalRecordsDto)
        {
            MedicalRecord medicalRecord = await _dbContext.MedicalRecords.FindAsync(medicalRecordsDto.Id);

            medicalRecord.Diagnosis = medicalRecordsDto.Diagnosis;
            medicalRecord.Notes = medicalRecordsDto.Notes;
            medicalRecord.PatientId = medicalRecordsDto.PatientId;

            await _dbContext.SaveChangesAsync();

            return medicalRecordsDto;
        }


    }
}
