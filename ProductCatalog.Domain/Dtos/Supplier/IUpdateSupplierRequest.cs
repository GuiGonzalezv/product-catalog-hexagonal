namespace ProductCatalog.Domain.Dtos.Supplier
{
    public interface IUpdateSupplierRequest : ICreateSupplierRequest
    {
        public string Id { get; set; }
    }
}
