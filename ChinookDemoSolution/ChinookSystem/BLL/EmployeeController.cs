using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System.ComponentModel; // for ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeCustomerList> Employee_EmployeeCustomerList()
        {
            using (var context = new ChinookSystemContext())
            {
				var results = context.Employees
								.Where(x => x.Title.Contains("Sales Support"))
								.OrderBy(x => x.LastName)
								.ThenBy(x => x.FirstName)
								.Select(x => new EmployeeCustomerList
								{
									EmployeeName = x.LastName + ", " + x.FirstName,
									Title = x.Title,
									NumberOfCustomers = x.Customers.Count(),
									Customers = (
													x.Customers
														.Select(y => new CustomerSupportItem
														{
															CustomerName = y.LastName + ", " + y.FirstName,
															State = y.State,
															City = y.City,
															Phone = y.Phone
														})
												)
								});

				return results.ToList();
			}
        }
    }
}
