using EmployeeManagement.CosmosDb;
using EmployeeManagement.Dto;
using EmployeeManagement.Dto.EmployeeManagementSystem.DTO;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeAdditionalDetailsService(ICosmosDbService cosmosDbService) : IEmployeeAdditionalDetails
    {
        private readonly ICosmosDbService _cosmosDbService = cosmosDbService;
        public async Task<EmployeeAdditionalDetails> AddEmployeeAdditionalDetails(AdditionalDetailsDto additionalDetailsDto, string employeeId)
        {
            var employeeBasicData= await _cosmosDbService.GetEmployeeBasicDetailsByEmployeeUId(employeeId);

            var employeeAdditionalDetail = new EmployeeAdditionalDetails
            {
                EmployeeBasicDetailsUId = employeeBasicData.EmployeeUId,
                AlternateEmail = additionalDetailsDto.AlternateEmail,
                AlternateMobile = additionalDetailsDto.AlternateMobile,
                WorkInformation = new WorkInfo_
                {
                    DesignationName = additionalDetailsDto.WorkInformation.DesignationName,
                    DepartmentName = additionalDetailsDto.WorkInformation.DepartmentName,
                    LocationName = additionalDetailsDto.WorkInformation.LocationName,
                    EmployeeStatus = additionalDetailsDto.WorkInformation.EmployeeStatus,
                    SourceOfHire = additionalDetailsDto.WorkInformation.SourceOfHire,
                    DateOfJoining = additionalDetailsDto.WorkInformation.DateOfJoining
                },
                PersonalDetails = new PersonalDetails_
                {
                    DateOfBirth = additionalDetailsDto.PersonalDetails.DateOfBirth,
                    Age = additionalDetailsDto.PersonalDetails.Age,
                    Gender = additionalDetailsDto.PersonalDetails.Gender,
                    Religion = additionalDetailsDto.PersonalDetails.Religion,
                    Caste = additionalDetailsDto.PersonalDetails.Caste,
                    MaritalStatus = additionalDetailsDto.PersonalDetails.MaritalStatus,
                    BloodGroup = additionalDetailsDto.PersonalDetails.BloodGroup,
                    Height = additionalDetailsDto.PersonalDetails.Height,
                    Weight = additionalDetailsDto.PersonalDetails.Weight
                },
                IdentityInformation = new IdentityInfo_
                {
                    PAN = additionalDetailsDto.IdentityInformation.PAN,
                    Aadhar = additionalDetailsDto.IdentityInformation.Aadhar,
                    Nationality = additionalDetailsDto.IdentityInformation.Nationality,
                    PassportNumber = additionalDetailsDto.IdentityInformation.PassportNumber,
                    PFNumber = additionalDetailsDto.IdentityInformation.PFNumber
                },

                Id= Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CreatedBy = "admin",
                CreatedByName = "admin",
                UpdatedBy = "admin",
                UpdatedByName = "admin",
                DocumentType = "emploee-additional",
                Version = 1,
                Active = true,
                Archived = false

            };
            return await _cosmosDbService.AddEmployeeAdditionDetails(employeeAdditionalDetail);
        }

        public async Task<string> DeleteEmployeeAdditionalDetails(string Id)
        {
            var employee = await _cosmosDbService.GetEmployeeAdditionDetailsById(Id);
            if (employee != null)
            {
                employee.Active = false;
                employee.Archived = true;
            await _cosmosDbService.DeleteEmployeeAdditionDetails(employee,Id);
                return $"Employee with ID {Id} deleted successfully";
            }
            else
            {
                return "employee not found";
            }
        }

        public async Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails()
        {
            return await _cosmosDbService.GetAllEmployeeAdditionDetails();
        }

        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsById(string Id)
        {
            return await _cosmosDbService.GetEmployeeAdditionDetailsById(Id);
        }

        public async Task<EmployeeAdditionalDetails> UpdateEmployeeAdditionalDetails(string Id, AdditionalDetailsDto additionalDetailsDto)
        {
            var data =await _cosmosDbService.GetEmployeeAdditionDetailsById(Id);
 
            if (data == null)
            {
                Console.WriteLine($"Additional details with ID {Id} not found.");
                return null;
            }


            var additionalDetails = new EmployeeAdditionalDetails
            {
            Id = Guid.NewGuid().ToString(),
                EmployeeBasicDetailsUId = data.EmployeeBasicDetailsUId,
                AlternateEmail = additionalDetailsDto.AlternateEmail,
            AlternateMobile = additionalDetailsDto.AlternateMobile,
            WorkInformation = new WorkInfo_
            {
                DesignationName = additionalDetailsDto.WorkInformation.DesignationName,
                DepartmentName = additionalDetailsDto.WorkInformation.DepartmentName,
                LocationName = additionalDetailsDto.WorkInformation.LocationName,
                EmployeeStatus = additionalDetailsDto.WorkInformation.EmployeeStatus,
                SourceOfHire = additionalDetailsDto.WorkInformation.SourceOfHire,
                DateOfJoining = additionalDetailsDto.WorkInformation.DateOfJoining
            },
            PersonalDetails = new PersonalDetails_
            {
                DateOfBirth = additionalDetailsDto.PersonalDetails.DateOfBirth,
                Age = additionalDetailsDto.PersonalDetails.Age,
                Gender = additionalDetailsDto.PersonalDetails.Gender,
                Religion = additionalDetailsDto.PersonalDetails.Religion,
                Caste = additionalDetailsDto.PersonalDetails.Caste,
                MaritalStatus = additionalDetailsDto.PersonalDetails.MaritalStatus,
                BloodGroup = additionalDetailsDto.PersonalDetails.BloodGroup,
                Height = additionalDetailsDto.PersonalDetails.Height,
                Weight = additionalDetailsDto.PersonalDetails.Weight
            },
            IdentityInformation = new IdentityInfo_
            {
                PAN = additionalDetailsDto.IdentityInformation.PAN,
                Aadhar = additionalDetailsDto.IdentityInformation.Aadhar,
                Nationality = additionalDetailsDto.IdentityInformation.Nationality,
                PassportNumber = additionalDetailsDto.IdentityInformation.PassportNumber,
                PFNumber = additionalDetailsDto.IdentityInformation.PFNumber
            },

                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DocumentType= "emploee-additional",
                CreatedBy = "admin",
                CreatedByName = "admin",
                UpdatedBy = "admin",
                UpdatedByName = "admin",
                Version = data.Version+=1,
                Active = true,
                Archived = false

            };

            return await _cosmosDbService.UpdateEmployeeAdditionDetails(data.Id, additionalDetails);
        }
    }
}
