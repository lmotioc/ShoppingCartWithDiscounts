using System.Collections.Generic;
using System.Linq;

public class DiscountRuleFactory : IChainFactory 
{
    private List<DiscountRule> _discountRules;
    public DiscountRuleFactory(IRepo repo) 
    {
        _discountRules = repo.DiscountRules;
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