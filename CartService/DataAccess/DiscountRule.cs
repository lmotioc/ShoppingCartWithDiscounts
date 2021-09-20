using System.Collections.Generic;
using System;
using System.Linq;

public class DiscountRule : DiscountChainHandler
{
    public string Name { get; set;}
    public override List<CartItem> Condition { get; set; }
    public override Func<decimal> Discount { get; set; }
}
