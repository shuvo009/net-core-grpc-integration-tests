using Grpc.DotNetCore.Common.DbModel;
using Microsoft.EntityFrameworkCore;

namespace Grpc.DotNetCore.Repository.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}