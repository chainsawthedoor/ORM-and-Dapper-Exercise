using System;
using System.Collections.Generic;
using System.Text;

namespace ORM_Dapper
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        
        public void CreateProduct(string name, double price, int categoryID);
    }
    
    
}
