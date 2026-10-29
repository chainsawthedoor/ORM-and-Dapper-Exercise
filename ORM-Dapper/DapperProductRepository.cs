using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * From products;");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                          "VALUES (@name, @price, @categoryID);"
                , new {name = name, price = price, categoryID = categoryID});
        }
    }
}
