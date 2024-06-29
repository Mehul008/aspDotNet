
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Dto;


namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeBasicDetailsController : ControllerBase
    {
        private readonly IEmployeeBasicDetails _employeeBasicDetails;

        public EmployeeBasicDetailsController(IEmployeeBasicDetails employeeBasicDetails)
        {
            _employeeBasicDetails = employeeBasicDetails ?? throw new ArgumentNullException(nameof(employeeBasicDetails));
        }


        [HttpPost("addEmployeeBasicDetails")]
        public async Task<IActionResult> AddEmployeeData(EmployeeBasicDto employeeBasicDto)
        {
            try
            {
                var response = await _employeeBasicDetails.AddEmployeeBasicData(employeeBasicDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("makePost")]
        public async Task<IActionResult> makePost(EmployeeBasicDto employeeBasicDto)
        {
            try
            {
                var response = await _employeeBasicDetails.MakePost(employeeBasicDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("filterEmployees")]
        public async Task<IActionResult> filterEmployees(FilterCriteria filterDto)
        {
            try
            {
                var response = await _employeeBasicDetails.FilterEmployees(filterDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("makeGet/{EmployeeUId}")]
        public async Task<IActionResult> makeGet(string EmployeeUId)
        {
            try
            {
                var response = await _employeeBasicDetails.MakeGet(EmployeeUId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("getAllEmployeeBasicDetails")]
        public async Task<IActionResult> GetEmployeeData()
        {
            try
            {
                var employees = await _employeeBasicDetails.GetAllEmployeeBasicDetails();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


   
        [HttpPost("getEmployeeBasicDetailsByEmployeeUId/{EmployeeUId}")]
        public async Task<IActionResult> GetEmployeeByEmployeeUId(string EmployeeUId)
        {
            try
            {
                var employee = await _employeeBasicDetails.GetEmployeeBasicDetailsByEmployeeUId(EmployeeUId);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {EmployeeUId} not found");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


   
        [HttpPost("deleteEmployeeBasicDetails/{EmployeeUId}")]
        public async Task<IActionResult> DeleteEmployeeData(string EmployeeUId)
        {
            try
            {
                var deletedEmployee = await _employeeBasicDetails.DeleteEmployeeBasicDetailsByEmployeeUId(EmployeeUId);
                if (deletedEmployee == null)
                {
                    return NotFound($"Employee with ID {EmployeeUId} not found");
                }
                return Ok($"Employee with ID {EmployeeUId} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("getEmployeeDetails/{EmployeeUId}")]
        public async Task<dynamic> GetEmployeeDetails(string EmployeeUId)
        {
                return await _employeeBasicDetails.GetEmployeeDetails(EmployeeUId);
        }


        [HttpPost("updateEmployeeBasicDetails/{EmployeeUId}")]
        public async Task<IActionResult> UpdateEmployeeData(string EmployeeUId, EmployeeBasicUpdateDto employeeBasicDto)
        {
            try
            {
                var updatedEmployee = await _employeeBasicDetails.UpdateEmployeeBasicDetails(EmployeeUId, employeeBasicDto);
                if (updatedEmployee == null)
                {
                    return NotFound($"Employee with ID {EmployeeUId} not found");
                }
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            await _employeeBasicDetails.ExportEmployeeDetails();
            return Ok();
        }
    }
}
