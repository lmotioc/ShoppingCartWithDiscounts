using System.Collections.Generic;
using System;
using System.Linq;

public class DiscountRule : DiscountChainHandler
{
    public override List<CartItem> Condition { get; set; }
    public override Func<decimal> Discount { get; set; }
}

public interface IChainFactory
{
    DiscountChainHandler CreateChain();
}
