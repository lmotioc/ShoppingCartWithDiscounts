using System.Collections.Generic;

public interface IRepo
{
    List<Product> Products { get; set; }
    List<DiscountRule> DiscountRules { get; set; }
}