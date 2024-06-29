using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class WorkInfo_
    {

        [JsonProperty("designationName", NullValueHandling = NullValueHandling.Ignore)]
        public string? DesignationName { get; set; }

        [JsonProperty("departmentName", NullValueHandling = NullValueHandling.Ignore)]
        public string? DepartmentName { get; set; }

        [JsonProperty("locationName", NullValueHandling = NullValueHandling.Ignore)]
        public string? LocationName { get; set; }

        [JsonProperty("employeeStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string? EmployeeStatus { get; set; }

        [JsonProperty("sourceOfHire", NullValueHandling = NullValueHandling.Ignore)]
        public string? SourceOfHire { get; set; }

        [JsonProperty("dateOfJoining", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfJoining { get; set; }
    }
}
