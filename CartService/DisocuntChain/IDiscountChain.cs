using System.Collections.Generic;
using System;

public interface IDiscountChain 
{
    List<Tuple<int, DiscountRule>> GetAppliedDiscounts(List<CartItem> cartItems);
}