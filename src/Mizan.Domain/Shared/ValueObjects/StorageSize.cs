// src/Mizan.Domain/Shared/ValueObjects/StorageSize.cs

namespace Mizan.Domain.Shared.ValueObjects;

public sealed record StorageSize
{
    public long Bytes { get; }

    private StorageSize(long bytes)
    {
        Bytes = bytes;
    }

    public static StorageSize FromBytes(long bytes)
    {
        if (bytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(bytes),
                "Storage size cannot be negative.");

        return new StorageSize(bytes);
    }

    public static StorageSize FromKilobytes(long kilobytes)
    {
        if (kilobytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(kilobytes),
                "Storage size cannot be negative.");

        return FromBytes(kilobytes * 1024L);
    }

    public static StorageSize FromMegabytes(long megabytes)
    {
        if (megabytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(megabytes),
                "Storage size cannot be negative.");

        return FromBytes(megabytes * 1024L * 1024L);
    }

    public static StorageSize FromGigabytes(long gigabytes)
    {
        if (gigabytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(gigabytes),
                "Storage size cannot be negative.");

        return FromBytes(gigabytes * 1024L * 1024L * 1024L);
    }

    public static StorageSize FromTerabytes(long terabytes)
    {
        if (terabytes < 0)
            throw new ArgumentOutOfRangeException(
                nameof(terabytes),
                "Storage size cannot be negative.");

        return FromBytes(
            terabytes * 1024L * 1024L * 1024L * 1024L);
    }

    public bool IsGreaterThan(StorageSize other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Bytes > other.Bytes;
    }

    public bool IsLessThan(StorageSize other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Bytes < other.Bytes;
    }

    public bool IsGreaterThanOrEqualTo(StorageSize other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Bytes >= other.Bytes;
    }

    public bool IsLessThanOrEqualTo(StorageSize other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Bytes <= other.Bytes;
    }

    public decimal ToKilobytes()
    {
        return Bytes / 1024m;
    }

    public decimal ToMegabytes()
    {
        return Bytes / (1024m * 1024m);
    }

    public decimal ToGigabytes()
    {
        return Bytes / (1024m * 1024m * 1024m);
    }

    public decimal ToTerabytes()
    {
        return Bytes / (1024m * 1024m * 1024m * 1024m);
    }
}
