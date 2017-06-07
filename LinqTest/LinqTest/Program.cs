using System;
using System.Collections.Generic;
using System.Linq;
using LinqTest.Utils;

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
            #region init MockData

            List<Company> companyList = MockData.GetCompanies();
            List<Employee> employeeList = MockData.GetEmployees();

            List<Customer> customers = MockData.GetCustomers();
            List<Product> products = MockData.GetProducts();
            List<Order> orders = MockData.GetOrders();
            List<OrderDetails> orderDetails = MockData.GetOrderDetails();

            #endregion
            
            #region Left Outer Join

            var leftOuterJoinQuery = employeeList
                                        .GroupJoin(
                                            companyList,
                                            e => e.CompanyId,
                                            c => c.CompanyId,
                                            (emp, cmps) => new { Employee = emp, Companies = cmps }
                                        )
                                        .SelectMany(
                                            empComps => empComps.Companies.DefaultIfEmpty(new Company { CompanyId = 0, CompanyName = string.Empty }),
                                            (empCompsR, cmpR) => new
                                            {
                                                EmployeeId = empCompsR.Employee.EmployeeId,
                                                EmployeeName = empCompsR.Employee.EmployeeName,
                                                CompanyId = cmpR.CompanyId,
                                                CompanyName = cmpR.CompanyName
                                            }
                                        );

            leftOuterJoinQuery.PrintQuery("Left Outer Join");

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

            rightOuterJoinQuery.PrintQuery("Right Outer Join");

            #endregion

            #region GroupBy and GroupBy...Having

            var employeesCountByCompany = rightOuterJoinQuery
                                                .GroupBy(e => e.CompanyId)
                                                //.Where(empGroup => empGroup.Count() > 1)    //it is GroupBy Having
                                                .Select(g =>
                                                    new
                                                    {
                                                        CompanyId = g.Key,
                                                        TotalEmployees = g.Count()
                                                    }
                                                );

            employeesCountByCompany.PrintQuery("GroupBy and GroupBy...Having");

            #endregion

            #region Left Outer Join another example

            var grpJoin = from e in employeeList
                          join c in companyList
                          on e.CompanyId equals c.CompanyId
                          into cmpGroup
                          from cg in cmpGroup.DefaultIfEmpty(new Company { CompanyId = 0, CompanyName = string.Empty })
                          select new
                          {
                              e.EmployeeId,
                              e.EmployeeName,
                              cg.CompanyId,
                              cg.CompanyName
                          };

            grpJoin.PrintQuery("Left Outer Join another example");

            #endregion

            #region Composite Equals with 4 tables join
            
            var compositeEqual = from c in customers
                                 join o in orders
                                 on c.Id equals o.CustomerId
                                 
                                 from p in products
                                 join od in orderDetails
                                 on new { ProductId = p.Id, OrderId = o.Id } equals new { ProductId = od.ProductId, OrderId = od.Id }
                                 into details
                                 from d in details
                                 select new
                                 {
                                     CustomerId = c.Id,
                                     OrderId = o.Id,
                                     ProductId = p.Id,
                                     Price = d.Price,
                                     DeliveryAddress = o.DeliveryAddress
                                 };

            compositeEqual.PrintQuery("Composite Equals with 4 tables join");

            #endregion
            
            Console.Read();
        }
    }
}
