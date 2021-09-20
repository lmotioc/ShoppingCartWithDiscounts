public class DiscountChain : IDiscountChain
{
    private readonly IChainFactory _chainFactory;

    public DiscountChain(IChainFactory chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public void UpdateDiscount(CustomerCart cart)
    {
        var chain = _chainFactory.CreateChain();
        chain.AddDiscount(cart);
    }
}