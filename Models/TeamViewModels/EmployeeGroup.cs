using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDocumentation.Models.TeamViewModels
{
    public class EmployeeGroup
    {
        public int? SupervisorID { get; set; }

        public int EmployeeCount { get; set; }
    }
}
