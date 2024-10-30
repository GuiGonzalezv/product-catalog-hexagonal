namespace ProductCatalog.Domain.Ports.Validation
{
    public class ValidationResultErrors
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
