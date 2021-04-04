using EmployeeDocumentation.Models;
using System;
using System.Linq;

namespace EmployeeDocumentation.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TeamContext context)
        {
            context.Database.EnsureCreated();

            // Look for any supervisors.
            if (context.Supervisors.Any())
            {
                return;   // DB has been seeded
            }

            var supervisors = new Supervisor[]
{
            new Supervisor{SupervisorID=9428541,FirstName="Matt",LastName="Smith",Extension=4889},
            new Supervisor{SupervisorID=8566741,FirstName="Brendan",LastName="Levi",Extension=4842},
            new Supervisor{SupervisorID=6638120,FirstName="Alice",LastName="Holt",Extension=4801},
            new Supervisor{SupervisorID=3325890,FirstName="Hillary",LastName="Jones",Extension=4867},
};
            foreach (Supervisor s in supervisors)
            {
                context.Supervisors.Add(s);
            }
            context.SaveChanges();


            var employee = new Employee[]
            {
            new Employee{EmployeeID=8566854,FirstName="Carson",LastName="Alexander",HireDate=DateTime.Parse("2005-09-01"),Extension=8854,SupervisorID=9428541},
            new Employee{EmployeeID=3699121,FirstName="Meredith",LastName="Alonso",HireDate=DateTime.Parse("2002-09-01"),Extension=8856,SupervisorID=9428541},
            new Employee{EmployeeID=9698541,FirstName="Arturo",LastName="Anand",HireDate=DateTime.Parse("2003-09-01"),Extension=8456,SupervisorID=8566741},
            new Employee{EmployeeID=8892327,FirstName="Gytis",LastName="Barzdukas",HireDate=DateTime.Parse("2002-09-01"),Extension=8636,SupervisorID=8566741},
            new Employee{EmployeeID=6634872,FirstName="Yan",LastName="Li",HireDate=DateTime.Parse("2002-09-01"),Extension=8124,SupervisorID=6638120},
            new Employee{EmployeeID=9636652,FirstName="Peggy",LastName="Justice",HireDate=DateTime.Parse("2001-09-01"),Extension=8752,SupervisorID=6638120},
            new Employee{EmployeeID=1145478,FirstName="Laura",LastName="Norman",HireDate=DateTime.Parse("2003-09-01"),Extension=8100,SupervisorID=3325890},
            new Employee{EmployeeID=2566397,FirstName="Nino",LastName="Olivetto",HireDate=DateTime.Parse("2005-09-01"),Extension=8090,SupervisorID=3325890}
            };
            foreach (Employee e in employee)
            {
                context.Employees.Add(e);
            }

            context.SaveChanges();

            var documentation = new Documentation[]
            {
            new Documentation{EmployeeID=3699121,AuthorInitials="MS",Category=Category.MonthlyReview,Date=DateTime.Parse("2020-09-01"),Entry="Meredith had a very strong month. She exceeded her goals for ACW and received 3 customer kudos. She is also very helpful to her teammates around her and can always be counted on to be helpful"},
            new Documentation{EmployeeID=9636652,AuthorInitials="AH",Category=Category.MonthlyReview,Date=DateTime.Parse("2020-09-01"),Entry="Peggy is struggling in a number of areas so we made our focus this month on improving her customer satisfaction scores. I challenged her to give the customers a reason to leave her a postitive review"},
            new Documentation{EmployeeID=9636652,AuthorInitials="AH",Category=Category.DisciplinaryAction,Date=DateTime.Parse("2020-09-15"),Entry="Peggy used unprofessional language with a customer who wanted a price match. We went over appropriate ways to handle this type of situation."},
            new Documentation{EmployeeID=6634872,AuthorInitials="AH",Category=Category.Awards,Date=DateTime.Parse("2020-09-03"),Entry="At the recognition meeting, Yan received the #5 MVP award for overall performance."},
            new Documentation{EmployeeID=9698541,AuthorInitials="BL",Category=Category.Kudos,Date=DateTime.Parse("2020-09-02"),Entry="Cusotmer left a voicemail saying that Arturo went above and beyond for her and she throoughly enjoyed the phone call. Because of this experience she plans to shop with us again!"},
            new Documentation{EmployeeID=2566397,AuthorInitials="HJ",Category=Category.Misc,Date=DateTime.Parse("2020-09-01"),Entry="Nino is hoping to get some time off around Thanksgiving. This is usually a busy season, but I agreed to work with Scheduling and see what we can do. Will follow up. "}
            };
            foreach (Documentation d in documentation)
            {
                context.Documentations.Add(d);
            }

            context.SaveChanges();
        }
    }
}