// src/Mizan.Infrastructure/Persistence/MizanDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace Mizan.Infrastructure.Persistence;

public class MizanDbContext : DbContext
{
    public MizanDbContext(
        DbContextOptions<MizanDbContext> options)
        : base(options)
    {
    }
}