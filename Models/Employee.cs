using System;
using System.Collections.Generic;


namespace EmployeeDocumentation.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }
        public int Extension { get; set; }
        public int SupervisorID { get; set; }

        public ICollection<Documentation> Documentation { get; set; }
    }
}
