using Abp.Application.Services.Dto;
using AutoMapper;
using MyApi.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Products.Mapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>().ForMember(x => x.NomeCliente, opt => opt.MapFrom(x => x.Cliente.Nome));
            CreateMap<PagedResult<Product>, PagedResultDto<ProductDto>> ();
        }
    }
}
