using System;
using EmployeeDemo.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EmployeeDemo.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}