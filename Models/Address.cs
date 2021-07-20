using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDemo.Models
{
    public class Address
    {
        [ForeignKey(name: "Employee")]
        public int Id { get; set; }
        public Employee Employee { get; set; }
        [Key]
        public int AddressId { get; set; }
        public string HouseNo { get; set; }
        public bool AddressType { get; set; } = false;
        public string Street { get; set; }
        public string City { get; set; } = "Delhi";
        public string State { get; set; } = "Delhi";
        public string Country { get; set; } = "India";
        public int Pincode { get; set; }
        public string Landmark { get; set; }
    }
}