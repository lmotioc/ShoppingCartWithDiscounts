using System;
using System.Collections.Generic;
using System.Linq;

public class CustomerCart
{
    public string CustomerId { get; set; }

    private List<CartItem> _items;
    public List<CartItem> Items
    {
        get { 
        // TODO can not call add on list
        return _items; }
        set { _items = value; 
        _discountChain.UpdateDiscount(this); 
        }
    }

    public List<Tuple<int, Func<decimal>>> Discounts { get; set; } = new List<Tuple<int, Func<decimal>>>();

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
                - Discounts.Select(i => i.Item1 * i.Item2()).Sum();
            return total;
        }
    }
}


