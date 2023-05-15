using Abp.Domain.Entities.Auditing;
using MyApi.Customers;

namespace MyApi.Products
{
    public class Product : FullAuditedEntity<long>
    {
        public string NomeProduto{ get; set; }

        public string CodigoProduto { get; set; }

        public Customer Cliente { get; set;}
        public long ClienteId { get; set;}

        public int Quantidade { get; set; }
    }
}
