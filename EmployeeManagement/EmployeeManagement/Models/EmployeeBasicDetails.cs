using Newtonsoft.Json;
using System.Net;

namespace EmployeeManagement.Models
{
    public class EmployeeBasicDetails : BaseEntity
    {
        [JsonProperty(PropertyName = "employeeUId")]
        public string EmployeeUId { get; set; }

        [JsonProperty(PropertyName = "employeeID")]
        public string EmployeeID { get; set; }

        [JsonProperty(PropertyName = "salutory")]
        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName")]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickName")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobile")]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "reportingManagerUId")]
        public string ReportingManagerUId { get; set; }

        [JsonProperty(PropertyName = "reportingManagerName")]
        public string ReportingManagerName { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}
