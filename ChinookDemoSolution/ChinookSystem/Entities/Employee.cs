using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChinookSystem.Entities
{
    internal partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Customers = new HashSet<Customer>();
            Employees1 = new HashSet<Employee>();
        }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(20, ErrorMessage = "Last name cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Title cannot be longer than 30 characters.")]
        public string Title { get; set; }

        public int? ReportsTo { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(70, ErrorMessage = "Address cannot be longer than 70 characters.")]
        public string Address { get; set; }

        [StringLength(40, ErrorMessage = "City cannot be longer than 40 characters.")]
        public string City { get; set; }

        [StringLength(40, ErrorMessage = "State cannot be longer than 40 characters.")]
        public string State { get; set; }

        [StringLength(40, ErrorMessage = "Country cannot be longer than 40 characters.")]
        public string Country { get; set; }

        [StringLength(10, ErrorMessage = "PostalCode cannot be longer than 10 characters.")]
        public string PostalCode { get; set; }

        [StringLength(24, ErrorMessage = "Phon cannot be longer than 24 characters.")]
        public string Phone { get; set; }

        [StringLength(24, ErrorMessage = "Fax cannot be longer than 24 characters.")]
        public string Fax { get; set; }

        [StringLength(60, ErrorMessage = "Email cannot be longer than 60 characters.")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}
