using EmployeeManagement.Dto;
using EmployeeManagement.Dto.EmployeeManagementSystem.DTO;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeAdditionalDetailsController : ControllerBase
    {
        private readonly IEmployeeAdditionalDetails _employeeAdditionalDetails;

        public EmployeeAdditionalDetailsController(IEmployeeAdditionalDetails employeeAdditionalDetails)
        {
            _employeeAdditionalDetails = employeeAdditionalDetails;
        }

   
        [HttpPost("addEmployeeAdditionDetailsByUId/{EmployeeUId}")]
        public async Task<IActionResult> AddEmployeeData(AdditionalDetailsDto employeeAdditionalDetailsDto,string EmployeeUId)
        {
            try
            {
                var response = await _employeeAdditionalDetails.AddEmployeeAdditionalDetails(employeeAdditionalDetailsDto, EmployeeUId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("getAllEmployeeAdditionDetails")]
        public async Task<IActionResult> GetEmployeeData()
        {
            try
            {
                var employees = await _employeeAdditionalDetails.GetAllEmployeeAdditionalDetails();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("getEmployeeAdditionDetailsById/{Id}")]
        public async Task<IActionResult> GetEmployeeById(string Id)
        {
            try
            {
                var employee = await _employeeAdditionalDetails.GetEmployeeAdditionalDetailsById(Id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {Id} not found");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("deleteEmployeeAdditionDetails/{Id}")]
        public async Task<IActionResult> DeleteEmployeeData(string Id)
        {
            try
            {
                var deletedEmployee = await _employeeAdditionalDetails.DeleteEmployeeAdditionalDetails(Id);
                if (deletedEmployee == null)
                {
                    return NotFound($"Employee with ID {Id} not found");
                }
                return Ok($"Employee with ID {Id} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("updateEmployeeAdditionDetails/{Id}")]
        public async Task<IActionResult> UpdateEmployeeData(string Id, AdditionalDetailsDto additionalDetailsDto)
        {
            try
            {
                var updatedEmployee = await _employeeAdditionalDetails.UpdateEmployeeAdditionalDetails(Id, additionalDetailsDto);
                if (updatedEmployee == null)
                {
                    return NotFound($"Employee with ID {Id} not found");
                }
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
