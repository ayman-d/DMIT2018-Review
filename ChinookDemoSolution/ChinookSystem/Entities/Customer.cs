using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChinookSystem.Entities
{
    internal partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(40, ErrorMessage = "First name cannot be longer than 40 characters long.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last name cannot be longer than 20 characters long.")]
        public string LastName { get; set; }

        [StringLength(80, ErrorMessage = "Company cannot be longer than 80 characters long.")]
        public string Company { get; set; }

        [StringLength(70, ErrorMessage = "Address cannot be longer than 70 characters long.")]
        public string Address { get; set; }

        [StringLength(40, ErrorMessage = "City cannot be longer than 40 characters long.")]
        public string City { get; set; }

        [StringLength(40, ErrorMessage = "State cannot be longer than 40 characters long.")]
        public string State { get; set; }

        [StringLength(40, ErrorMessage = "Country cannot be longer than 40 characters long.")]
        public string Country { get; set; }

        [StringLength(10, ErrorMessage = "Postalcode cannot be longer than 10 characters long.")]
        public string PostalCode { get; set; }

        [StringLength(24, ErrorMessage = "Phone cannot be longer than 24 characters long.")]
        public string Phone { get; set; }

        [StringLength(24, ErrorMessage = "Fax cannot be longer than 24 characters long.")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(60, ErrorMessage = "Email cannot be longer than 60 characters long.")]
        public string Email { get; set; }

        public int? SupportRepId { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
