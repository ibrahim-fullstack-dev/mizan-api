namespace Mizan.Domain.Entities.Platform;

public class Plan
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public long StorageLimitBytes { get; private set; }

    public decimal MonthlyPrice { get; private set; }

    public decimal YearlyPrice { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    // Navigation Property
    public ICollection<Subscription> Subscriptions { get; private set; }
        = new List<Subscription>();
}