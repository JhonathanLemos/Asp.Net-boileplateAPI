using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyApi.GetAlls;
using MyApi.Products;
using MyApi.Products.Dto;
using System.Collections.Generic;

namespace MyApi.Customers.Dto
{
    [AutoMapFrom(typeof(Customer))]
    public class SelectProductsCustomerDto : GetAll
    {
        public long CustomerId { get; set; }
    }
}
