using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/linq/join-by-using-composite-keys
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/join-clause
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<Company> companyList = MockData.GetCompanies();
            List<Employee> employeeList = MockData.GetEmployees();

            #region Left Outer Join

            var leftOuterJoinQuery = employeeList
                                        .GroupJoin(
                                            companyList,
                                            emp => emp.CompanyId,
                                            company => company.CompanyId,
                                            (emp, cmps) => new { Employee = emp, Companies = cmps }
                                        ).SelectMany(
                                            empComps => empComps.Companies.DefaultIfEmpty(),
                                            (empCompsR, cmpR) => new
                                            {
                                                EmployeeId = empCompsR.Employee.EmployeeId,
                                                EmployeeName = empCompsR.Employee.EmployeeName,
                                                CompanyId = cmpR == null ? string.Empty : cmpR.CompanyId.ToString(),
                                                CompanyName = cmpR == null ? string.Empty : cmpR.CompanyName
                                            }
                                        );

            Console.WriteLine("************** Left Outer Join **************");
            foreach (var item in leftOuterJoinQuery)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region Right Outer Join

            var rightOuterJoinQuery = companyList
                                        .GroupJoin(
                                            employeeList,
                                            company => company.CompanyId,
                                            emp => emp.CompanyId,
                                            (cmp, emps) => new { Company = cmp, Employees = emps }
                                        ).SelectMany(
                                            compEmps => compEmps.Employees.DefaultIfEmpty(),
                                            (compEmpsR, empR) => new
                                            {
                                                CompanyId = compEmpsR.Company.CompanyId,
                                                CompanyName = compEmpsR.Company.CompanyName,
                                                EmployeeId = empR == null ? string.Empty : empR.EmployeeId.ToString(),
                                                EmployeeName = empR == null ? string.Empty : empR.EmployeeName
                                            }
                                        );

            Console.WriteLine("************** Right Outer Join **************");
            foreach (var item in rightOuterJoinQuery)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region GroupBy and GroupBy...Having

            Console.WriteLine("************** GroupBy and GroupBy...Having **************");
            var employeesCountByCompany = rightOuterJoinQuery
                                                .GroupBy(e => e.CompanyId)
                //.Where(empGroup => empGroup.Count() > 1)
                                                .Select(g =>
                                                    new
                                                    {
                                                        CompanyId = g.Key,
                                                        TotalEmployees = g.Count()
                                                    }
                                                );

            foreach (var item in employeesCountByCompany)
            {
                Console.WriteLine(string.Format("CompanyId:{0};TotalEmployees={1};", item.CompanyId, item.TotalEmployees));
            }

            #endregion

            #region Left Outer Join another example

            var grpJoin = from e in employeeList
                          join c in companyList
                          on e.CompanyId equals c.CompanyId
                          into cmpGroup
                          select new
                          {
                              e.EmployeeId,
                              e.EmployeeName,
                              //cmpGroup,
                              CompanyId = cmpGroup.Any() ? cmpGroup.FirstOrDefault().CompanyId.Value.ToString() : "",
                              CompanyName = cmpGroup.Any() ? cmpGroup.FirstOrDefault().CompanyName : ""
                          };

            foreach (var item in grpJoin)
            {
                Console.WriteLine(string.Format("EmployeeId:{0};EmployeeName={1};CompanyId={2};CompanyName={3};", item.EmployeeId, item.EmployeeName, item.CompanyId, item.CompanyName));//item.cmpGroup
            }

            #endregion

            #region Composite Equals with 3 tables join

            List<Product> products = MockData.GetProducts();
            List<Order> orders = MockData.GetOrders();
            List<OrderDetails> orderDetails = MockData.GetOrderDetails();

            var compositeEqual = from p in products
                                 from o in orders
                                 join od in orderDetails
                                 on new { ProductId = p.Id, OrderId = o.Id } equals new { ProductId = od.ProductId, OrderId = od.Id }
                                 into details
                                 from d in details
                                 select new {
                                     OrderId = o.Id,
                                     ProductId = p.Id, 
                                     Price = d.Price
                                 };

            Console.WriteLine("************** Composite Equals with 3 tables join **************");
            foreach (var item in compositeEqual)
            {
                Console.WriteLine(item);
            }

            #endregion
            
            Console.Read();
        }
    }
}
