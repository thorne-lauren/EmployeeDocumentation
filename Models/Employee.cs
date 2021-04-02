﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeDocumentation.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }
        public int Extension { get; set; }
        public int SupervisorID { get; set; }

        public ICollection<Documentation> Documentation { get; set; }
    }
}
