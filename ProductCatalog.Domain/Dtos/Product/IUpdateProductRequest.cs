namespace ProductCatalog.Domain.Dtos.Product
{
    public interface IUpdateProductRequest : ICreateProductRequest
    {
        public string Id { get; set; }
    }
}
