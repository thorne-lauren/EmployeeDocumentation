using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDocumentation.Models
{
    public enum Category
    {
        MonthlyReview,
        YearEndReview,
        DisciplinaryAction,
        Kudos,
        Awards,
        Misc
    }
    
    public class Documentation
    {
        public int DocumentationID { get; set; }
        public int EmployeeID { get; set; }
        public int SupervisorID { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Entry { get; set; }

        public Employee Employee { get; set; }
        public Supervisor Supervisor { get; set; }
    }
}
