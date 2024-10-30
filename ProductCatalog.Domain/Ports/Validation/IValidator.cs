namespace ProductCatalog.Domain.Ports.Validation
{
    public interface IValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T entity);
        ValidationResult Validate(T entity);
    }
}
