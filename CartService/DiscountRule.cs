using System.Collections.Generic;
using System;
using System.Linq;

public abstract class DiscountChainHandler
{
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

    public int Applies(List<CartItem> condition, List<CartItem> cartItems) 
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

    public abstract List<CartItem> Condition { get; set; }
    public abstract Func<decimal> Discount { get; set; }
}

public class DiscountRule : DiscountChainHandler
{
    public override List<CartItem> Condition { get; set; }
    
    public override Func<decimal> Discount { get; set; }
}

public interface IChainFactory
{
    DiscountChainHandler CreateChain();
}

public class DiscountRuleFactory : IChainFactory 
{
    private List<DiscountRule> _discountRules;
    public DiscountRuleFactory() 
    {
        _discountRules = FakeRepo.DiscountRules;
    }
    public DiscountChainHandler CreateChain()
    {
        DiscountChainHandler previous = null; 
        foreach(DiscountRule discount in _discountRules )
        {
            previous?.nextHandler(discount);
            previous = discount;
        }

       return _discountRules.First(); 
    }
}

public class ChainCreationHandler 
{
    private readonly IChainFactory _chainFactory;

    public ChainCreationHandler(IChainFactory chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public void UpdateDiscount(CustomerCart cart)
    {
        var chain = _chainFactory.CreateChain();
        chain.AddDiscount(cart);
    }
}