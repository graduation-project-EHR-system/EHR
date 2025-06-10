using EHR.Core.Dtos;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "Admin")]
    public class HospitalController : ControllerBase
    {
       
        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpPost]
        public async Task<ActionResult<HospitalDto>> AddHospitalAsync(HospitalDto hospitalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<HospitalDto>(400, hospitalDto, "Invalid Model"));

            var result = await _hospitalService.AddHospitalAsync(hospitalDto);

            if (result is null)
                return BadRequest(new ResponseWithData<HospitalDto>(400, hospitalDto, "Invalid Model"));

            return Ok(new ResponseWithData<HospitalDto>(201, result, "Medical medicalRecord Is Added Successfully"));
        }


        [HttpGet("get-all-hospitals")]
        public async Task<ActionResult<ResponseWithAllHospital>> GetAllHospitals([FromQuery] PaginationRequest paginationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<PaginationRequest>(400, paginationRequest, "Invalid Model"));

            var paginationResponse = await _hospitalService.GetHospitalsAsync(paginationRequest);

            if (paginationResponse is null)
                return NotFound(new ApiResponse(400, "There Is No Data In this Page Index"));

            return Ok(new ResponseWithAllHospital(200, paginationResponse, "Hospitals Are Retrived Successfully"));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalDto>> GetHospitalById([FromRoute] Guid id)
        {

            if (id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result = await _hospitalService.GetHospitalByIdAsync(id);

            if (result is null)
                return Ok(new ResponseWithData<HospitalDto>(200, null, "There Is No Hospital For This ID"));

            return Ok(new ResponseWithData<HospitalDto>(200, result, "Hospital are Retrived Successfully"));
        }

        [HttpPut]
        public async Task<ActionResult<HospitalDto>> UpdateMedicalRecord(HospitalDto hospitalDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<HospitalDto>(400, hospitalDto, "Invalid Model"));

            if (hospitalDto.Id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result = await _hospitalService.UpdateHospitalAsync(hospitalDto);

            if (result is null)
                return NotFound(new ApiResponse(400, "Invalid ID"));

            return Ok(new ResponseWithData<HospitalDto>(200, result, "Hospital Is Updated Successfully !"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalDto>> DeleteHospitalById([FromRoute] Guid id)
        {

            if (id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result  = await _hospitalService.DeleteHospitalAsync(id);

            if (!result)
                return Ok(new ResponseWithData<HospitalDto>(200, null, "There Is No Hospital For This ID"));

            return Ok(new ApiResponse(200, "Hospital Is Deleted Successfully!"));
        }

        
    }
}
