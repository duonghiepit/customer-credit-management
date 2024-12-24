using CreditManagement.Models.Entities;

namespace CreditManagement.Models
{
    public class CustomerListViewModel
    {
        public List<Customer> Customers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCustomers { get; set; }
        public string SearchString { get; set; }
        public string FilterType { get; set; }
        public string FilterValue { get; set; }
    }
}
