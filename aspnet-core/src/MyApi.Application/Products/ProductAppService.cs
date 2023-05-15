using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using MyApi.Authorization;
using MyApi.Customers.Dto;
using MyApi.GetAlls;
using MyApi.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Products
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, long, GetAll, ProductDto>, IProductAppService
    {
        private readonly IObjectMapper _objectMapper;

        public ProductAppService(IRepository<Product, long> repository, IObjectMapper objectMapper) : base(repository)
        {
            _objectMapper = objectMapper;

        }

        [AbpAuthorize(AdministrativePermissionActions.Administrative_Product_Edit)]
        public override Task<ProductDto> CreateAsync(ProductDto input)
        {
            return base.CreateAsync(input);
        }


        [AbpAuthorize(AdministrativePermissionActions.Administrative_Product_Edit)]
        public override Task<ProductDto> UpdateAsync(ProductDto input)
        {
            return base.UpdateAsync(input);
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetAll input)
        {
            return Repository.GetAllIncluding(x => x.Cliente).WhereIf(!string.IsNullOrEmpty(input.Keyword), x => x.NomeProduto.Contains(input.Keyword));
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCustomerId(SelectProductsCustomerDto input)
        {
            return base.GetAllAsync(input).Result.Items.Where(x => x.ClienteId == input.CustomerId);
        }
    }
}
