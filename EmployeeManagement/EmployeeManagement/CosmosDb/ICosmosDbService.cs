using EmployeeManagement.Dto;
using EmployeeManagement.Dto.EmployeeManagementSystem.DTO;
using EmployeeManagement.Models;

namespace EmployeeManagement.CosmosDb
{
    public interface ICosmosDbService
    {
        Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails();
        Task<EmployeeBasicDetails> AddEmployeeBasicDetails(EmployeeBasicDetails employeeBasicDetail);
        Task<string> UpdateEmployeeBasicDetails(string EmployeeUId, EmployeeBasicDetails employeeBasicDetails);
        Task<string> DeleteEmployeeBasicDetails(EmployeeBasicDetails basicDetails,string EmployeeUId);

        Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByEmployeeUId(string EmployeeUId);



        Task<EmployeeAdditionalDetails> AddEmployeeAdditionDetails(EmployeeAdditionalDetails additionalDetails);
        Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionDetails();
        Task<EmployeeAdditionalDetails> UpdateEmployeeAdditionDetails(string id, EmployeeAdditionalDetails additionalDetails);
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionDetailsById(string id);
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionDetailsByEmpId(string EmployeeUId);
        Task<string> DeleteEmployeeAdditionDetails(EmployeeAdditionalDetails additionalDetails,string id);
        Task<List<EmployeeBasicDetails>> AddEmployeeBasicDetails(FilterCriteria filterDto);
    }
}
