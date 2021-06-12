using System;
using System.Data.Entity;
using EmployeeDemo.Helpers;
using EmployeeDemo.Models;

namespace EmployeeDemo.Data
{
    public class EmployeeContext : DbContext
    {        
        public EmployeeContext() : base("name=EmployeeContext"){}
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

    }
}

