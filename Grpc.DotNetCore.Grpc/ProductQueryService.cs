using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.DotNetCore.Common.Interfaces;
using Grpc.DotNetCore.Grpc.Protos;

namespace Grpc.DotNetCore.Grpc
{
    public class ProductQueryService : ProductQueryGrpcService.ProductQueryGrpcServiceBase
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override async Task<ProductQueryResponse> GetProduct(ProductQueryRequest request,
            ServerCallContext context)
        {
            var productId = Guid.Parse(request.Id);
            var product = await _productRepository.GetProduct(productId);
            return new ProductQueryResponse
            {
                Label = product.Label,
                Name = product.Name
            };
        }
    }
}