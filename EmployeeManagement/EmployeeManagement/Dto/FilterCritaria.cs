namespace EmployeeManagement.Dto
{
    public class FilterCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string SortBy { get; set; }
    }

    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> Records { get; set; }
    }
}
