using Microsoft.EntityFrameworkCore;
using ReembolsoCorporativo.Api.Domain.Entities;

namespace ReembolsoCorporativo.Api.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Despesa> Despesas => Set<Despesa>();
}
