using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDocumentation.Models
{
    public class Supervisor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupervisorID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int extension { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
