using Microsoft.AspNetCore.Mvc;
using IBASEmployeeService.Models;

namespace IBASEmployeeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        // GET: api/employee/getemployees
        [HttpGet("GetEmployees")]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = new List<Employee>
            {
                // --- Salg ---
                new Employee {
                    Id = "21",
                    Name = "Mette Bangsbo",
                    Email = "meba@ibas.dk",
                    Department = new Department { Id = 1, Name = "Salg" }
                },
                // --- Support ---
                new Employee {
                    Id = "22",
                    Name = "Hans Merkel",
                    Email = "hame@ibas.dk",
                    Department = new Department { Id = 2, Name = "Support" }
                },
                new Employee {
                    Id = "23",
                    Name = "Karsten Mikkelsen",
                    Email = "kami@ibas.dk",
                    Department = new Department { Id = 2, Name = "Support" }
                },
                // --- IT ---
                new Employee {
                    Id = "24",
                    Name = "Sofie Nielsen",
                    Email = "soni@ibas.dk",
                    Department = new Department { Id = 3, Name = "IT" }
                },
                new Employee {
                    Id = "25",
                    Name = "Morten Berg",
                    Email = "mobe@ibas.dk",
                    Department = new Department { Id = 3, Name = "IT" }
                },
                new Employee {
                    Id = "26",
                    Name = "Rikke Madsen",
                    Email = "rima@ibas.dk",
                    Department = new Department { Id = 3, Name = "IT" }
                },
                // --- Kantinen ---
                new Employee {
                    Id = "27",
                    Name = "Lone Andersen",
                    Email = "loan@ibas.dk",
                    Department = new Department { Id = 4, Name = "Kantinen" }
                },
                new Employee {
                    Id = "28",
                    Name = "Thomas Juul",
                    Email = "thju@ibas.dk",
                    Department = new Department { Id = 4, Name = "Kantinen" }
                }
            };

            _logger.LogInformation("Returning {Count} employees", employees.Count);
            return Ok(employees);
        }

        // GET: api/employee/department/{departmentName}
        [HttpGet("department/{departmentName}")]
        public ActionResult<IEnumerable<Employee>> GetByDepartment(string departmentName)
        {
            // Genbrug data fra ovenstående metode
            var employees = ((OkObjectResult)GetEmployees().Result!).Value as IEnumerable<Employee>;

            var filtered = employees!
                .Where(e => e.Department != null &&
                            e.Department.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filtered.Count == 0)
                return NotFound($"Ingen ansatte fundet i afdelingen '{departmentName}'.");

            _logger.LogInformation("Returning {Count} employees from {Department}", filtered.Count, departmentName);
            return Ok(filtered);
        }
    }
}
