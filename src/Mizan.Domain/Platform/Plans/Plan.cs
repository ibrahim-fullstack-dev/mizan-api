// src/Mizan.Domain/Platform/Plans/Plan.cs

using Mizan.Domain.Shared.Primitives;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Domain.Platform.Plans;

public sealed class Plan : AggregateRoot
{
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public StorageSize StorageLimit { get; private set; } = null!;

    public Money MonthlyPrice { get; private set; } = null!;

    public Money YearlyPrice { get; private set; } = null!;

    public PlanStatus Status { get; private set; }

    // EF Core constructor
    private Plan()
    {
    }

    private Plan(
        string name,
        string? description,
        StorageSize storageLimit,
        Money monthlyPrice,
        Money yearlyPrice)
    {
        Name = name;
        Description = description;
        StorageLimit = storageLimit;
        MonthlyPrice = monthlyPrice;
        YearlyPrice = yearlyPrice;
        Status = PlanStatus.Active;
    }

    public static Plan Create(
        string name,
        string? description,
        StorageSize storageLimit,
        Money monthlyPrice,
        Money yearlyPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Plan name is required.",
                nameof(name));

        ArgumentNullException.ThrowIfNull(storageLimit);
        ArgumentNullException.ThrowIfNull(monthlyPrice);
        ArgumentNullException.ThrowIfNull(yearlyPrice);

        return new Plan(
            name.Trim(),
            string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim(),
            storageLimit,
            monthlyPrice,
            yearlyPrice);
    }

    public void UpdateDetails(
        string name,
        string? description,
        StorageSize storageLimit,
        Money monthlyPrice,
        Money yearlyPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Plan name is required.",
                nameof(name));

        ArgumentNullException.ThrowIfNull(storageLimit);
        ArgumentNullException.ThrowIfNull(monthlyPrice);
        ArgumentNullException.ThrowIfNull(yearlyPrice);

        Name = name.Trim();

        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();

        StorageLimit = storageLimit;
        MonthlyPrice = monthlyPrice;
        YearlyPrice = yearlyPrice;
    }

    public void Deactivate()
    {
        if (Status == PlanStatus.Inactive)
            return;

        Status = PlanStatus.Inactive;
    }

    public void Activate()
    {
        if (Status == PlanStatus.Active)
            return;

        Status = PlanStatus.Active;
    }
}