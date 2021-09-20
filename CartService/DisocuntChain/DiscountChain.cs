using System.Collections.Generic;
using System;

public class DiscountChain : IDiscountChain
{
    private readonly IChainFactory _chainFactory;

    public DiscountChain(IChainFactory chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public List<Tuple<int, Func<decimal>>> GetAppliedDiscount(List<CartItem> cartItems)
    {
        var chain = _chainFactory.CreateChain();
        return chain.GetDiscount(cartItems, new List<Tuple<int, Func<decimal>>>());
    }
}