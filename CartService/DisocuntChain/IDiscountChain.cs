using System.Collections.Generic;
using System;

public interface IDiscountChain 
{
    List<Tuple<int, Func<decimal>>> GetAppliedDiscount(List<CartItem> cartItems);
}