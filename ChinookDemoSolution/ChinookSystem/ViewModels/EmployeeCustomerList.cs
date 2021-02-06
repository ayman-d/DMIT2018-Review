using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class EmployeeCustomerList
    {
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public int NumberOfCustomers { get; set; }
        public IEnumerable<CustomerSupportItem> Customers { get; set; }
    }
}
