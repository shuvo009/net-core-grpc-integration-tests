using System;
using System.Threading.Tasks;
using Grpc.DotNetCore.Common.DbModel;
using Grpc.DotNetCore.Common.Interfaces;
using Grpc.DotNetCore.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Grpc.DotNetCore.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbSet<ProductModel> _model;

        public ProductRepository(ProductDbContext profileDbContext)
        {
            _model = profileDbContext.Products;
        }

        public Task<ProductModel> GetProduct(Guid id)
        {
            return _model.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}