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
    }
}