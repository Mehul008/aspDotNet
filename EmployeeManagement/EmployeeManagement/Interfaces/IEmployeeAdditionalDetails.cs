using EmployeeManagement.Dto;
using EmployeeManagement.Dto.EmployeeManagementSystem.DTO;
using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeAdditionalDetails
    {
        Task<EmployeeAdditionalDetails> AddEmployeeAdditionalDetails(AdditionalDetailsDto additionalDetailsDto, string employeeId);
        Task<string> DeleteEmployeeAdditionalDetails(string Id);
        Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails();
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsById(string Id);
        Task<EmployeeAdditionalDetails> UpdateEmployeeAdditionalDetails(string Id, AdditionalDetailsDto additionalDetailsDto);
    }
}
