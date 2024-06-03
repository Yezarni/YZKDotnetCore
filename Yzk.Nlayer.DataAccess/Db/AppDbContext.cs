using Microsoft.EntityFrameworkCore;
using Yzk.RestApiwithNlayer.Model;

namespace Yzk.Nlayer.DataAccess.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.GetSqlConnectionStringBuilder().ConnectionString);
    }
    public DbSet<BlogModel> Blog { get; set; }
}
