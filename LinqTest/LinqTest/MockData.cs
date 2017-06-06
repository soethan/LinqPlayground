using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    public class MockData
    {
        public static List<Company> GetCompanies()
        {
            var list = new List<Company> {
                new Company { CompanyId = 1, CompanyName = "Company 1" },
                new Company { CompanyId = 2, CompanyName = "Company 2" },
                new Company { CompanyId = 3, CompanyName = "Company 3" }
            };
            return list;
        }

        public static List<Employee> GetEmployees()
        {
            var list = new List<Employee>
            {
                new Employee { EmployeeId = 1, EmployeeName = "Employee 1", CompanyId = 1 },
                new Employee { EmployeeId = 2, EmployeeName = "Employee 2", CompanyId = 2 },
                new Employee { EmployeeId = 3, EmployeeName = "Employee 3", CompanyId = 2 },
                new Employee { EmployeeId = 4, EmployeeName = "Employee 4" }
            };
            return list;
        }
    }
}
