using System;
using System.Collections.Generic;
using System.Linq;

public class ProductRepo
{
    private List<Product> _products;
    public ProductRepo(IRepo repo)
    {
        _products = repo.Products;
    }

    public List<Product> GetAll() {
        return _products ;
    }

    public Product GetByName(string name) {
        return _products.Where(product => product.Name == name).FirstOrDefault();
    }
    
}