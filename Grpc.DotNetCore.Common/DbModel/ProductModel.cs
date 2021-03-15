using System;

namespace Grpc.DotNetCore.Common.DbModel
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
    }
}