using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDocumentation.Models
{
    public enum Category
    {
        [Display(Name = "Monthly Review")]
        MonthlyReview,
        [Display(Name = "Year-End Review")]
        YearEndReview,
        [Display(Name = "Disciplinary Action")]
        DisciplinaryAction,
        Kudos,
        Awards,
        Misc
    }
    
    public class Documentation
    {
        [Key]
        public int DocumentationID { get; set; }
        [Display(Name = "Employee Number")]
        public int EmployeeID { get; set; }
        [StringLength(10)]
        [Display(Name = "Author Initials")]
        public string AuthorInitials { get; set; }
        public Category Category { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Entry { get; set; }
        public Employee Employee { get; set; }
    }
}
