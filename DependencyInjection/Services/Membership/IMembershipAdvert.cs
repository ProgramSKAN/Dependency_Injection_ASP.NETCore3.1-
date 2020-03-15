namespace DependencyInjection.Services.Membership
{
    public interface IMembershipAdvert
    {
        decimal OfferPrice { get; }
        decimal Saving { get; }
    }
}