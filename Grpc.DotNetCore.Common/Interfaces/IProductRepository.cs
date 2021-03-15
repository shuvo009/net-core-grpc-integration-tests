using System;
using System.Threading.Tasks;
using Grpc.DotNetCore.Common.DbModel;

namespace Grpc.DotNetCore.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductModel> GetProduct(Guid id);
    }
}