using Microsoft.EntityFrameworkCore;
using YZKDotnetCore.NLayer.DataAccess;
using YZKDotnetCore.NLayer.DataAccess.Model;

namespace DotNetTrainingBatch4.NLayer.DataAccess.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
    }

    public DbSet<BlogModel> Blogs { get; set; }
}