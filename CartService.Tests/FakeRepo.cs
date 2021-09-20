using System.Collections.Generic;
using System.Linq;

public class FakeRepo
{
    public List<Product> Products
    {
        get
        {
            return new List<Product>()
            {
                new Product {
                    Id = 1,
                    Name = "Butter",
                    Price = 0.8m
                },
                new Product {
                    Id = 2,
                    Name = "Milk",
                    Price = 1.15m
                },
                new Product {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.0m
                }
            };
        }
    }


    public List<DiscountRule> DiscountRules
    {
        get
        {
            return new List<DiscountRule>()
            {
                new DiscountRule
                {
                    Name = "Buy two butters and get one bread 50% off",
                    Condition = new List<CartItem> ()
                    {
                        new CartItem
                        {
                            Product = Products.Where(p => p.Name == "Butter").FirstOrDefault(),
                            Quantity = 2
                        },
                        new CartItem
                        {
                            Product = Products.Where(p => p.Name == "Bread").FirstOrDefault(),
                            Quantity = 1
                        }
                    },
                    Discount = () => { return Products.Where(p => p.Name == "Bread").FirstOrDefault().Price / 2; }
                },
                new DiscountRule
                {
                    Name = "Buy 3 milks and get 4th milk for free",
                    Condition = new List<CartItem> ()
                    {
                        new CartItem
                        {
                            Product = Products.Where(p => p.Name == "Milk").FirstOrDefault(),
                            Quantity = 4
                        }
                    },
                    Discount = () => { return Products.Where(p => p.Name == "Milk").FirstOrDefault().Price; }
                }
            };
        }
    }
}