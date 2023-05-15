using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using MyApi.GetAlls;
using MyApi.Products;
using MyApi.Products.Dto;

namespace MyApi.Tests.Products
{
    public class ProductsAppService_Tests : MyApiTestBase
    {
        private readonly IProductAppService _productAppService;

        public ProductsAppService_Tests()
        {
            _productAppService = Resolve<IProductAppService>();
        }

        [Fact]
        public async Task GetProducts_Test()
        {
            // Act
            var output = await _productAppService.GetAllAsync((new GetAll { MaxResultCount = 20, SkipCount = 0 }));

            // Assert
            output.Items.Count.ShouldBe(0);
        }

        [Fact]
        public async Task CreateProducts_Test()
        {
            // Act
            await _productAppService.CreateAsync(
                new ProductDto
                {
                    ClienteId = 1,
                    CodigoProduto = "123123123",
                    NomeProduto = "Nome Produto",
                    Quantidade = 18
                });

            await UsingDbContextAsync(async context =>
            {
                var product = await context.Product.FirstOrDefaultAsync(u => u.CodigoProduto == "123123123");
                product.ShouldNotBeNull();
            });
        }
    }
}
