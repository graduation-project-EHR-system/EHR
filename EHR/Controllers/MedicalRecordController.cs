﻿using EHR.Core.Dtos;
using EHR.Core.Response;
using EHR.Core.ServicesContract;
using MedicalRecords.Service.Api.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecordDto>> CreateMedicalRecord(MedicalRecordDto medicalRecordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<MedicalRecordDto>(400, medicalRecordDto, "Invalid Model"));

            var result = await _medicalRecordService.CreateMedicalRecordAsync(medicalRecordDto);

            if (result is null)
            {
                return BadRequest(new ApiResponse(400, "Patient Or Doctor Does not Exist"));
            }

            return Ok(new ResponseWithData<MedicalRecordDto>(200, result, "Medical medicalRecord Is Added Successfully"));
        }


        [HttpGet("get-all-medical-records")]
        public async Task<ActionResult<ResponseWithAllMedicalRecordData>> GetAllMedicalRecords([FromQuery] PaginationRequest paginationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<PaginationRequest>(400, paginationRequest, "Invalid Model"));

            var paginationResponse = await _medicalRecordService.GetAllMedicalRecordsAsync(paginationRequest);

            if (paginationResponse is null)
                return NotFound(new ApiResponse(400, "There Is No Data In this Page Index"));

            return Ok(new ResponseWithAllMedicalRecordData(200, paginationResponse, "Medical Records Are Retrived Successfully"));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecordsById([FromRoute] Guid id)
        {

            if (id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result = await _medicalRecordService.GetMedicalRecordByIdAsync(id);

            if (result is null)
                return Ok(new ResponseWithData<MedicalRecordDto>(200, null, "There Is No more Medical Records For This ID"));

            return Ok(new ResponseWithData<MedicalRecordDto>(200, result, "Medical Records are Retrived Successfully"));
        }

        [HttpPut]
        public async Task<ActionResult<UpdateMedicalRecordDto>> UpdateMedicalRecord(UpdateMedicalRecordDto updateMedicalRecordDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<UpdateMedicalRecordDto>(400, updateMedicalRecordDto, "Invalid Model"));

            if (updateMedicalRecordDto.Id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result = await _medicalRecordService.UpdateMedicalRecordAsync(updateMedicalRecordDto);

            if (result is null)
                return NotFound(new ApiResponse(400, "There Is Invalid ID for Patient Or Doctor Or Medical Record"));

            return Ok(new ResponseWithData<UpdateMedicalRecordDto>(200, result, "Medical Record Is Updated Successfully"));
        }
    }
}
