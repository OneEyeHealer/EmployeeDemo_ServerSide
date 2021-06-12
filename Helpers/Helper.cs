using EmployeeDemo.Data;
using EmployeeDemo.Models;
using System.Data.Entity;
using System.Linq;

namespace EmployeeDemo.Helpers
{
    //interface IDataCheck
    //{
    //    bool EmployeeDuplication(Employee data);
    //}
    public class Helper
    {
        private readonly EmployeeContext db;
        public Helper()
        {
            db = new EmployeeContext();
        }
        public bool IsPhoneDuplicate(Employee data)
        {
            var employee = db.Employees.Where(d => d.Phone == data.Phone && d.Id != data.Id).ToList();
            bool isDuplicate = employee.Count > 1;
            return isDuplicate;
        }
        public bool IsHouseNoDuplicate(Address data)
        {
            var address = db.Addresses.Where(d => d.HouseNo == data.HouseNo && d.AddressId != data.AddressId).ToList();
            bool isDuplicate = address.Count > 1;
            return isDuplicate;
        }
        public bool IsDefaultAddress(Address data)
        {
            var address = db.Addresses.Where(d => d.AddressType == true && d.Id == data.Id).ToList();
            bool isDefault = address.Count > 1;
            return isDefault;
        }
        public void SetDefaultAddress(int id, Address data)
        {
            var individualEmpAddress = db.Addresses.Where(d => d.Id == data.Id).ToList();
            foreach (var address in individualEmpAddress)
            {
                address.AddressType = address.AddressId == id && address.AddressType;
                db.SaveChanges();
            }
            //db.Entry(address).State = EntityState.Modified;
        }

        public void SetAllAddress(DbSet<Employee> data)
        {
            foreach (var item in data)
            {
                var address = db.Addresses.Where(d => d.Id == item.Id).ToList();
                item.Addresses = address;
            }
        }
        public void SetAddressById(Employee data)
        {
                var address = db.Addresses.Where(d => d.Id == data.Id).ToList();
                data.Addresses = address;
        }
    }
}
