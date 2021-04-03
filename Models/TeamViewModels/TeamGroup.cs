using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDocumentation.Models.TeamViewModels
{
    public class TeamGroup
    {
        public int? SupervisorID { get; set; }
        public Supervisor Supervisor { get; set; }

        public int EmployeeCount { get; set; }
    }
}
