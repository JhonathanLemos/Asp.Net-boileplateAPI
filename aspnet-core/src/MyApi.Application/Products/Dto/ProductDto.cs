using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyApi.Products.Dto
{
    [AutoMapFrom(typeof(Product))]
    public class ProductDto : FullAuditedEntityDto<long>
    {
        public string NomeProduto { get; set; }

        public string CodigoProduto { get; set; }

        public long ClienteId { get; set; }

        public int Quantidade { get; set; }

        public string NomeCliente { get; set; }

    }
}
