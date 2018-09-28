using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePhoneBook.Models
{
    public class EmployeeContact
    { 
        /// <summary>
        /// Employee ID number
        /// </summary>        
        public string EmployeeID { get; set; }

        /// <summary>
        /// Employee's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Employee phone number.
        /// </summary>
        public string PhoneNumber  { get; set; }

        /// <summary>
        /// Employee job title.
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Employee's email address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Employee's office location.
        /// </summary>
        public string Location { get; set; }

    }

}
