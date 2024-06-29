using EmployeeManagement.CosmosDb;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using OfficeOpenXml;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagement.Services
{
    public class EmployeeBasicDetailsService(ICosmosDbService cosmosDbService) : IEmployeeBasicDetails
    {
        private readonly ICosmosDbService _cosmosDbService = cosmosDbService;

        public async Task<EmployeeBasicDetails> AddEmployeeBasicData(EmployeeBasicDto employeeBasicDto)
        {
            var employeeBasicDetail = new EmployeeBasicDetails
            {
                EmployeeUId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                EmployeeID = employeeBasicDto.EmployeeID,
                Salutory = employeeBasicDto.Salutory,
                FirstName = employeeBasicDto.FirstName,
                MiddleName = employeeBasicDto.MiddleName,
                LastName = employeeBasicDto.LastName,
                NickName = employeeBasicDto.NickName,
                Email = employeeBasicDto.Email,
                Mobile = employeeBasicDto.Mobile,
                Role = employeeBasicDto.Role,
                ReportingManagerUId = employeeBasicDto.ReportingManagerUId,
                ReportingManagerName = employeeBasicDto.ReportingManagerName,
                Address = employeeBasicDto.Address,



                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DocumentType = "employee-basic",
                CreatedBy = "admin",
                CreatedByName = "admin",
                UpdatedBy = "admin",
                UpdatedByName = "admin",
                Version = 1,
                Active = true,
                Archived = false

            };

            return await _cosmosDbService.AddEmployeeBasicDetails(employeeBasicDetail);
        }

        public async Task<string> DeleteEmployeeBasicDetailsByEmployeeUId(string EmployeeUId)
        {
            var employee = await _cosmosDbService.GetEmployeeBasicDetailsByEmployeeUId(EmployeeUId);
            if (employee != null)
            {
                employee.Active = false;
                employee.Archived = true;
                await _cosmosDbService.DeleteEmployeeBasicDetails(employee, EmployeeUId);
                return $"Employee with ID {EmployeeUId} deleted successfully";
            }
            else
            {
                return "employee not found";
            }
        }

        public async Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails()
        {
            return await _cosmosDbService.GetAllEmployeeBasicDetails();
        }

        public async Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByEmployeeUId(string EmployeeUId)
        {
            return await _cosmosDbService.GetEmployeeBasicDetailsByEmployeeUId(EmployeeUId);
        }

        public Task<string> MakePost(EmployeeBasicDto apiRequestData)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };


            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                StringContent apiRequestContent = new StringContent(JsonConvert.SerializeObject(apiRequestData), Encoding.UTF8, "application/json");

                var httpResponse = httpClient.PostAsync("https://localhost:7237/api/EmployeeBasicDetails/addEmployeeBasicDetails", apiRequestContent).Result;
                var httpResponseString = httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    // todo log failure response 
                    throw new Exception();
                }
                return httpResponseString;
            }
        }

        public Task<string> MakeGet(string id)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);

                var urlString = "https://localhost:7237/api/EmployeeBasicDetails/getEmployeeBasicDetailsByEmployeeUId/" + id;
                var httpResponse = httpClient.GetAsync(urlString).Result;
                var httpResponseString = httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    // todo log failure response 
                    throw new Exception();
                }
                return httpResponseString;
            }
        }

        public async Task<string> UpdateEmployeeBasicDetails(string EmployeeUId, EmployeeBasicUpdateDto employeeBasicDto)
        {
            var employee = await _cosmosDbService.GetEmployeeBasicDetailsByEmployeeUId(EmployeeUId);
            if (employee != null)
            {
                var employeeBasicDetail = new EmployeeBasicDetails
                {
                    EmployeeUId = employee.EmployeeUId,
                    Id = Guid.NewGuid().ToString(),
                    EmployeeID = employee.EmployeeID,
                    Salutory = employeeBasicDto.Salutory,
                    FirstName = employeeBasicDto.FirstName,
                    MiddleName = employeeBasicDto.MiddleName,
                    LastName = employeeBasicDto.LastName,
                    NickName = employeeBasicDto.NickName,
                    Email = employeeBasicDto.Email,
                    Mobile = employeeBasicDto.Mobile,
                    Role = employee.Role,
                    ReportingManagerUId = employee.ReportingManagerUId,
                    ReportingManagerName = employee.ReportingManagerName,
                    Address = employee.Address,

                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    DocumentType = "employee-basic",
                    CreatedBy = "admin",
                    CreatedByName = "admin",
                    UpdatedBy = "admin",
                    UpdatedByName = "admin",
                    Version = employee.Version += 1,
                    Active = true,
                    Archived = false

                };

                return await _cosmosDbService.UpdateEmployeeBasicDetails(employee.Id, employeeBasicDetail);
            }
            else
            {
                return "Error in Updating Details";
            }
        }

        public async Task<List<EmployeeBasicDetails>> FilterEmployees(FilterCriteria filterDto)
        {
            return await _cosmosDbService.AddEmployeeBasicDetails(filterDto);
        }


        public async Task<List<dynamic>> GetEmployeeDetails(string EmployeeUId)
        {
            var empBasicDetail = await _cosmosDbService.GetEmployeeBasicDetailsByEmployeeUId(EmployeeUId);

            if (empBasicDetail == null)
            {
                return null;
            }

            var empBasicEmployeeUId = empBasicDetail.EmployeeUId;

            var employeeAdditionalDetails = await _cosmosDbService.GetEmployeeAdditionDetailsByEmpId(EmployeeUId);
            var combinedDetails = new
            {
                EmployeeBasicDetails = empBasicDetail,
                EmployeeAdditionalDetails = employeeAdditionalDetails
            };

            var resultList = new List<dynamic>();
            resultList.Add(combinedDetails);

            return resultList;
        }

        public async Task<string> ExportEmployeeDetails()
        {
            string filePath = "C:\\Users\\admin\\Desktop\\centraLogic\\EmployeeExport.xlsx";

            var employeeList = await _cosmosDbService.GetAllEmployeeBasicDetails();
            var employeeAdditionalDetailsList = await _cosmosDbService.GetAllEmployeeAdditionDetails();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Add headers
                string[] headers = new string[]
                {
            "Salutory", "FirstName", "MiddleName", "LastName", "NickName", "Email", "Mobile",
            "EmployeeID", "Role", "ReportingManagerUEmployeeUId", "ReportingManagerName", "Address", "EmployeeUId",
            "DocumentType", "CreatedBy", "CreatedByName", "CreatedOn", "UpdatedBy",
            "UpdatedByName", "UpdatedOn", "Version", "Active", "Archived",
            "AlternateEmail", "AlternateMobile", "DesignationName", "DepartmentName", "LocationName", "EmployeeStatus",
            "SourceOfHire", "DateOfJoining", "DateOfBirth", "Age", "Gender", "Religion", "Caste", "MaritalStatus",
            "BloodGroup", "Height", "Weight", "PAN", "Aadhar", "Nationality", "PassportNumber", "PFNumber"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Add data
                for (int row = 0; row < employeeList.Count; row++)
                {
                    var employee = employeeList[row];
                    var additionalDetails = employeeAdditionalDetailsList.FirstOrDefault(e => e.EmployeeBasicDetailsUId == employee.EmployeeUId);

                    worksheet.Cells[row + 2, 1].Value = employee.Salutory;
                    worksheet.Cells[row + 2, 2].Value = employee.FirstName;
                    worksheet.Cells[row + 2, 3].Value = employee.MiddleName;
                    worksheet.Cells[row + 2, 4].Value = employee.LastName;
                    worksheet.Cells[row + 2, 5].Value = employee.NickName;
                    worksheet.Cells[row + 2, 6].Value = employee.Email;
                    worksheet.Cells[row + 2, 7].Value = employee.Mobile;
                    worksheet.Cells[row + 2, 8].Value = employee.EmployeeID;
                    worksheet.Cells[row + 2, 9].Value = employee.Role;
                    worksheet.Cells[row + 2, 10].Value = employee.ReportingManagerUId;
                    worksheet.Cells[row + 2, 11].Value = employee.ReportingManagerName;
                    worksheet.Cells[row + 2, 12].Value = employee.Address;
                    worksheet.Cells[row + 2, 13].Value = employee.EmployeeUId;
                    worksheet.Cells[row + 2, 14].Value = employee.DocumentType;
                    worksheet.Cells[row + 2, 15].Value = employee.CreatedBy;
                    worksheet.Cells[row + 2, 16].Value = employee.CreatedByName;
                    worksheet.Cells[row + 2, 17].Value = employee.CreatedOn.ToString();
                    worksheet.Cells[row + 2, 18].Value = employee.UpdatedBy;
                    worksheet.Cells[row + 2, 19].Value = employee.UpdatedByName;
                    worksheet.Cells[row + 2, 20].Value = employee.UpdatedOn.ToString();
                    worksheet.Cells[row + 2, 21].Value = employee.Version.ToString();
                    worksheet.Cells[row + 2, 22].Value = employee.Active.ToString();
                    worksheet.Cells[row + 2, 23].Value = employee.Archived.ToString();

                    if (additionalDetails != null)
                    {
                        worksheet.Cells[row + 2, 24].Value = additionalDetails.AlternateEmail;
                        worksheet.Cells[row + 2, 25].Value = additionalDetails.AlternateMobile;
                        worksheet.Cells[row + 2, 26].Value = additionalDetails.WorkInformation.DesignationName;
                        worksheet.Cells[row + 2, 27].Value = additionalDetails.WorkInformation.DepartmentName;
                        worksheet.Cells[row + 2, 28].Value = additionalDetails.WorkInformation.LocationName;
                        worksheet.Cells[row + 2, 29].Value = additionalDetails.WorkInformation.EmployeeStatus;
                        worksheet.Cells[row + 2, 30].Value = additionalDetails.WorkInformation.SourceOfHire;
                        worksheet.Cells[row + 2, 31].Value = additionalDetails.WorkInformation.DateOfJoining.ToString();
                        worksheet.Cells[row + 2, 32].Value = additionalDetails.PersonalDetails.DateOfBirth.ToString();
                        worksheet.Cells[row + 2, 33].Value = additionalDetails.PersonalDetails.Age;
                        worksheet.Cells[row + 2, 34].Value = additionalDetails.PersonalDetails.Gender;
                        worksheet.Cells[row + 2, 35].Value = additionalDetails.PersonalDetails.Religion;
                        worksheet.Cells[row + 2, 36].Value = additionalDetails.PersonalDetails.Caste;
                        worksheet.Cells[row + 2, 37].Value = additionalDetails.PersonalDetails.MaritalStatus;
                        worksheet.Cells[row + 2, 38].Value = additionalDetails.PersonalDetails.BloodGroup;
                        worksheet.Cells[row + 2, 39].Value = additionalDetails.PersonalDetails.Height;
                        worksheet.Cells[row + 2, 40].Value = additionalDetails.PersonalDetails.Weight;
                        worksheet.Cells[row + 2, 41].Value = additionalDetails.IdentityInformation.PAN;
                        worksheet.Cells[row + 2, 42].Value = additionalDetails.IdentityInformation.Aadhar;
                        worksheet.Cells[row + 2, 43].Value = additionalDetails.IdentityInformation.Nationality;
                        worksheet.Cells[row + 2, 44].Value = additionalDetails.IdentityInformation.PassportNumber;
                        worksheet.Cells[row + 2, 45].Value = additionalDetails.IdentityInformation.PFNumber;
                    }
                }

                worksheet.Cells.AutoFitColumns();
                System.IO.File.WriteAllBytes(filePath, package.GetAsByteArray());
            }

            return filePath;
        }
    }
}
