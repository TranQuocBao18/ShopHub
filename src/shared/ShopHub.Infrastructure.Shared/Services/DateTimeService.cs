using ShopHub.Infrastructure.Shared.Interfaces;

namespace ShopHub.Infrastructure.Shared.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}