using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class ContextBase1 : DbContext
{
    public ContextBase1(DbContextOptions<ContextBase1> options) : base(options)
    {
    }

    public DbSet<ImageBase> Images { get; set; }
}

public class ContextBase2 : DbContext
{
    public ContextBase2(DbContextOptions<ContextBase2> options) : base(options)
    {
    }

    public DbSet<ImageCopy> ImagesCopies { get; set; }
}