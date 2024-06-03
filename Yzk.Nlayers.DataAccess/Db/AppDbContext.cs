using Microsoft.EntityFrameworkCore;
using Yzk.RestApiwithNlayer.Model;

namespace Yzk.RestApiwithNlayer.Db;

internal class  AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blog { get; set; }
}
