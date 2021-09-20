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

    public List<Tuple<int, Func<decimal>>> Discounts
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
                - Discounts.Select(i => i.Item1 * i.Item2()).Sum();
            return total;
        }
    }
}


