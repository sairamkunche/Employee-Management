using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Contracts.Models.Entities
{
    public partial class Employee
    {
        [Key]
        [Required]
        public int EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Skill { get; set; }
        public int YearsExperience { get; set; }
    }
}
