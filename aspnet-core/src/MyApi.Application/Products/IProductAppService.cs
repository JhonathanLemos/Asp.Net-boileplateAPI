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
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, long, GetAll, ProductDto>
    {
        Task<IEnumerable<ProductDto>> GetProductsByCustomerId(SelectProductsCustomerDto input);
    }
}
