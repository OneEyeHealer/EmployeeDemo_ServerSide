using System.Linq;
using EmployeeDemo.Data;
using EmployeeDemo.Models;

namespace EmployeeDemo.Controllers
{
    internal  interface Duplication
    {
        bool EmployeeDuplication(Employee data);
    }
    public class DataValidation 
    {
        private readonly EmployeeContext db;
        public DataValidation(){
            db = new EmployeeContext();
        }   
        public bool EmployeeDuplication(Employee data)
        {
            var employee = db.Employees.Where(d => d.Phone == data.Phone).ToList();
            bool isDuplicate = employee.Count >= 1;
            return isDuplicate;
        }
    }
}