using System;
using System.Collections.Generic;
using System.Linq;

public class CustomerCart
{
    public string CustomerId { get; set; }

    private List<CartItem> _items;
    public List<CartItem> Items
    {
        get
        {
            return _items;
        }
        set
        {
            _items = value;
        }
    }

    public List<Tuple<int, DiscountRule>> Discounts
    {
        get
        {
            return _discountChain.GetAppliedDiscounts(Items);
        }
    }

    private IDiscountChain _discountChain;

    public CustomerCart(IDiscountChain discountChain)
    {
        _items = new List<CartItem>();
        _discountChain = discountChain;
    }

    public CustomerCart(string customerId)
    {
        CustomerId = customerId;
    }

    public decimal FullPrice
    {
        get
        {
            return Items.Select(i => i.Quantity * i.Product.Price).Sum();
        }
    }

    public decimal TotalPrice
    {
        get
        {
            var total =
                Items.Select(i => i.Quantity * i.Product.Price).Sum()
                - Discounts.Select(i => i.Item1 * i.Item2.Discount()).Sum();
            Console.WriteLine(this.ToString());
            Console.WriteLine($"Total: ${total}");
            return total;
        }
    }

    public override string ToString()
    {
        var items = Items.Select(i => i.Quantity + "\tx\t" + i.Product.Name + "\t$" + i.Product.Price + "\n").ToList();
        var discounts = Discounts.Select(i => i.Item1 + "\tx\t" + i.Item2.Name + "\n").ToList();
        return $"Items \n{string.Join("", items.ToArray())} \nDiscounts \n{string.Join("", discounts.ToArray())}"; 
    }
}


