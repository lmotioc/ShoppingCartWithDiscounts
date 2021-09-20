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

    public List<Tuple<int, Func<decimal>>> GetDiscount(List<CartItem> cartItems , List<Tuple<int, Func<decimal>>> discountRules) 
    {
        var applies = Applies(Condition, cartItems);
        if (applies>0)
        {
            discountRules.Add(new Tuple<int, Func<decimal>> (applies, this.Discount));
        }
        discountHandler?.GetDiscount(cartItems, discountRules);
        return discountRules;
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