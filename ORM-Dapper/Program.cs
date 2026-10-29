using System;
using System.Collections;
using System.Collections.Generic;
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
            Console.WriteLine("What do you want to name your Product?");
            var productName = Console.ReadLine();
            Console.WriteLine("What do you want the price for the new product to be?");
            var productPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("What do you want the category ID for the new product to be?");
            var categoryID = Convert.ToInt32(Console.ReadLine());
            
            productRepository.CreateProduct(productName, productPrice, categoryID);
            
            var products = productRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductID} Name: {product.Name}");
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