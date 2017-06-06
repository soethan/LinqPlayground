using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    public class MockData
    {
        public static List<Product> GetProducts()
        {
            var list = new List<Product> {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" },
                new Product { Id = 3, Name = "Product 3" }
            };
            return list;
        }

        public static List<Order> GetOrders()
        {
            var list = new List<Order> {
                new Order { Id = 1, DeliveryAddress = "DeliveryAddress 1" },
                new Order { Id = 2, DeliveryAddress = "DeliveryAddress 2" },
                new Order { Id = 3, DeliveryAddress = "DeliveryAddress 3" }
            };
            return list;
        }

        public static List<OrderDetails> GetOrderDetails()
        {
            var list = new List<OrderDetails> {
                new OrderDetails { Id = 1, ProductId = 1, Price = 10 },
                new OrderDetails { Id = 1, ProductId = 2, Price = 20 },
                new OrderDetails { Id = 3, ProductId = 3, Price = 30 }
            };
            return list;
        }

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
