using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.DotNetCore.Common.DbModel;
using Grpc.DotNetCore.Grpc.Protos;
using Grpc.DotNetCore.IntegrationTest.Setup;
using Grpc.DotNetCore.Repository.Data;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Grpc.DotNetCore.IntegrationTest
{
    public class ProductQueryServiceTest
    {
        [Fact]
        public async Task Get_product_by_id_via_grpc()
        {
            var host = new TestHost();
            var serviceScope = host.Services.CreateScope();
            var products = await InsertMockProducts(serviceScope);

            var product = products[new Random().Next(products.Count - 1)];

            var client = host.CreateDefaultClient();
            var grpcChannel = GrpcChannel.ForAddress(client.BaseAddress, new GrpcChannelOptions
            {
                HttpClient = client
            });

            var productQueryGrpcServiceClient = new ProductQueryGrpcService.ProductQueryGrpcServiceClient(grpcChannel);
            var productQueryResponse = await productQueryGrpcServiceClient.GetProductAsync(new ProductQueryRequest
            {
                Id = product.Id.ToString()
            });
            Assert.Equal(product.Name, productQueryResponse.Name);
            Assert.Equal(product.Label, productQueryResponse.Label);

        }

        #region Supported Methods

        private async Task<List<ProductModel>> InsertMockProducts(IServiceScope serviceScope, int numberOfProduct = 20)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>();

            var products = Enumerable.Range(0, numberOfProduct).Select(x => new ProductModel
            {
                Id = Guid.NewGuid(),
                Label = $"Label {x}",
                Name = $"Name {x}"
            }).ToList();
            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
            return products;
        }
        #endregion
    }
}
