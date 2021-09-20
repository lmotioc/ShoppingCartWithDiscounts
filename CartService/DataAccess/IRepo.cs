using System;
using System.Collections.Generic;
using System.Linq;


public interface IRepo
{
    List<Product> Products { get; set; }
    List<DiscountRule> DiscountRules { get; set; }
}