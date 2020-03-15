namespace DependencyInjection.Services.Membership
{
    public interface IMembershipAdvertBuilder
    {
        MembershipAdvert Build();
        MembershipAdvertBuilder WithDiscount(decimal discount);
    }
}