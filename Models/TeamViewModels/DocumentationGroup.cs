using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDocumentation.Models.TeamViewModels
{
    public class DocumentationGroup
    {
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int DocumentationCount { get; set; }
    }
}
