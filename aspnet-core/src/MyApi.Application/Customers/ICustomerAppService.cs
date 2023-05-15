
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MyApi.Authorization;
using MyApi.Customers.Dto;
using MyApi.GetAlls;
using MyApi.Products;
using MyApi.Products.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Customers
{
    public interface ICustomerAppService : IAsyncCrudAppService<CustomerDto, long, GetAll, CreateCustomerDto>
    {
       
    }
}
