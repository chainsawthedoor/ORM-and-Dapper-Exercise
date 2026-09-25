using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Drawing.Printing;
//^^^^MUST HAVE USING DIRECTIVES^^^^

namespace ORM_Dapper
{
    public class Program
    {
       #region Configuration
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
        #endregion
            
            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            var productRepository = new DapperProductRepository(conn);
            var products = productRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductID} Name: {product.Name} Category ID: {product.CategoryID} On Sale: {product.OnSale} Stock Level: {product.StockLevel}");
            }

            #region Departments
            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter. . .");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();
            Print(depos);
            

            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the new Department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }

            Console.WriteLine("Have a great day.");
        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }

        }
        #endregion

    }
}