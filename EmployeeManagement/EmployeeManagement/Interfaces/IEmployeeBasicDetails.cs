using EmployeeManagement.Dto;
using EmployeeManagement.Models;
using Microsoft.Azure.Cosmos;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeBasicDetails
    {
        Task<string> MakeGet(string EmployeeUId);
        Task<string> MakePost(EmployeeBasicDto employeeBasicDto);
        Task<EmployeeBasicDetails> AddEmployeeBasicData(EmployeeBasicDto employeeBasicDto);
        Task<string> DeleteEmployeeBasicDetailsByEmployeeUId(string EmployeeUId);
        Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails();
        Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByEmployeeUId(string EmployeeUId);
        Task<string> UpdateEmployeeBasicDetails(string EmployeeUId, EmployeeBasicUpdateDto employeeBasicDto);
        Task<List<EmployeeBasicDetails>> FilterEmployees(FilterCriteria filterDto);

        Task<List<dynamic>> GetEmployeeDetails(string EmployeeUId);

        Task<string> ExportEmployeeDetails();
    }
}
