using Microsoft.EntityFrameworkCore;
using Yzk.Nlayer.DataAccesses.Models;


namespace Yzk.Nlayer.DataAccesses.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blog { get; set; }
}
