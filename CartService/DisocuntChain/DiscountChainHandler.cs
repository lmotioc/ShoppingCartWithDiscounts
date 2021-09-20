using System.Collections.Generic;
using System;
using System.Linq;

public abstract class DiscountChainHandler
{
    public abstract List<CartItem> Condition { get; set; }
    public abstract Func<decimal> Discount { get; set; }

    public DiscountChainHandler discountHandler;

    public void nextHandler(DiscountChainHandler discountHandler)
    {
        this.discountHandler = discountHandler;
    }

    public CustomerCart AddDiscount(CustomerCart cart) 
    {
        var applies = Applies(Condition, cart.Items);
        if (applies>0)
        {
            cart.Discounts.Add(new Tuple<int, Func<decimal>> (applies, this.Discount));
        }
        return discountHandler?.AddDiscount(cart);
    }

    private int Applies(List<CartItem> condition, List<CartItem> cartItems) 
    {
        var maxApplied = int.MaxValue;
        foreach(var item in condition)
        {
            var cartItem = cartItems.Where(ci => ci.Product.Id == item.Product.Id).FirstOrDefault();
            if(cartItem == null) return 0;
            if(maxApplied > cartItem.Quantity/item.Quantity){
                maxApplied = cartItem.Quantity/item.Quantity;
            }
        }
        return maxApplied;
    }

}