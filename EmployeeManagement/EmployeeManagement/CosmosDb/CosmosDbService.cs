
using EmployeeManagement.Dto;
using EmployeeManagement.Models;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace EmployeeManagement.CosmosDb
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService()
        {
            _container = GetContainer();
        }

        public async Task<EmployeeBasicDetails> AddEmployeeBasicDetails(EmployeeBasicDetails employeeBasicDetail)
        {
            return await _container.CreateItemAsync<EmployeeBasicDetails>(employeeBasicDetail);
        }

        public async Task<string> DeleteEmployeeBasicDetails(EmployeeBasicDetails basicDetails, string EmployeeUId)
        {
            await _container.ReplaceItemAsync<EmployeeBasicDetails>(basicDetails, EmployeeUId);
            return "Record Deleted";
        }

        public async Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails()
        {
            var query = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(a => a.DocumentType == "employee-basic" && !a.Archived && a.Active).ToList();
            List<EmployeeBasicDetails> results = new List<EmployeeBasicDetails>();
            return query;
        }



        public async Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByEmployeeUId(string EmployeeUId)
        {
            var query = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(a => a.EmployeeUId == EmployeeUId && a.DocumentType== "employee-basic" && !a.Archived && a.Active).FirstOrDefault();
            return query;
        }

        public async Task<string> UpdateEmployeeBasicDetails(string EmployeeUId, EmployeeBasicDetails employeeBasicDetails)
        {
            await _container.ReplaceItemAsync<EmployeeBasicDetails>(employeeBasicDetails, EmployeeUId);
            return "Record Updated";
        }

















        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionDetailsByEmpId(string EmpId)
        {
            var query = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true)
                                .Where(a => a.EmployeeBasicDetailsUId == EmpId
                                            && a.DocumentType == "emploee-additional"
                                            && !a.Archived
                                            && a.Active)
                                .FirstOrDefault();

            return await Task.FromResult(query);
        }
        public async Task<EmployeeAdditionalDetails> AddEmployeeAdditionDetails(EmployeeAdditionalDetails additionalDetailsDto)
        {
            return await _container.CreateItemAsync<EmployeeAdditionalDetails>(additionalDetailsDto);
        }

        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionDetailsById(string Id)
        {
            return _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(a => a.Id == Id && a.DocumentType== "emploee-additional" && !a.Archived && a.Active).FirstOrDefault();
        }

        public async Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionDetails()
        {
            return _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(a => a.DocumentType == "emploee-additional" && !a.Archived && a.Active).ToList();
        }

        public async Task<EmployeeAdditionalDetails> UpdateEmployeeAdditionDetails(string Id, EmployeeAdditionalDetails additionalDetails)
        {
            var response = await _container.ReplaceItemAsync<EmployeeAdditionalDetails>(additionalDetails,Id);
            return response;
        }


        public async Task<string> DeleteEmployeeAdditionDetails(EmployeeAdditionalDetails additionalDetails, string Id)
        {
            await _container.ReplaceItemAsync<EmployeeAdditionalDetails>(additionalDetails, Id);
            return "Record Deleted";
        }



        public async Task<List<EmployeeBasicDetails>> AddEmployeeBasicDetails(FilterCriteria filterDto)
        {
            var queryString = $@"
    SELECT * FROM c 
    WHERE c.Active = true 
      AND c.Archived = false 
      AND (
        CONTAINS(c.FirstName, '{filterDto.FirstName}') 
        OR CONTAINS(c.LastName, '{filterDto.LastName}') 
        OR CONTAINS(c.Role, '{filterDto.Role}')
      ) 
    ORDER BY c.UpdatedOn {filterDto.SortBy}";


            var queryDefinition = new QueryDefinition(queryString);
            var queryResultSetIterator = _container.GetItemQueryIterator<EmployeeBasicDetails>(queryDefinition);

            var response=new List<EmployeeBasicDetails>();

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                response.AddRange(currentResultSet);
            }

            return response;
        }




        private static Container GetContainer()
        {
            string URI = "https://localhost:8081";
            string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
            string DatabaseName = "EmployeeMgmt";
            string ContainerName = "Employee";

            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

    }
}
