using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using EmployeeDemo.Data;
using EmployeeDemo.Helpers;
using EmployeeDemo.Models;

namespace EmployeeDemo.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeContext db;
        private readonly Helper dv;
        EmployeeController()
        {
            db = new EmployeeContext();
            dv = new Helper();


        }

        // GET: api/Employee
        public IEnumerable<Employee> GetEmployees()
        {
            var data = db.Employees;
            dv.SetAllAddress(data);
            //foreach(var item in data)
            //{
            //    var address = db.Addresses.Where(d => d.Id == item.Id).ToList();
            //    item.Addresses = address;
            //}
            return data;
        }

        // GET: api/Employee/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            dv.SetAddressById(employee);
            return Ok(employee);
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != employee.Id) return BadRequest();

            if (dv.IsPhoneDuplicate(employee)) return BadRequest("Duplicate Property");

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok(employee);
        }

        // POST: api/Employee
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (dv.IsPhoneDuplicate(employee)) return BadRequest("Duplicate Property");
            
            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}