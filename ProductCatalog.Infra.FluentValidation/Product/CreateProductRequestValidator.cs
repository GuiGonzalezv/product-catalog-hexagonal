
using FluentValidation;
using ProductCatalog.Domain.Dtos.Product;
using ProductCatalog.Domain.Ports.Validation;

namespace ProductCatalog.Infra.FluentValidation.Product
{
    public class CreateProductRequestValidator : AbstractValidator<ICreateProductRequest>, Domain.Ports.Validation.IValidator<ICreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("É necessário informar o nome do produto.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("É necessário informar a descrição do produto.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("O preço do produto deve ser maior que 0.");

            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                .WithMessage("A quantidade de estoque deve ser maior que 0.");

            RuleFor(x => x.SupplierId)
                .NotEmpty()
                .WithMessage("É necessário informar o fornecedor do produto.");

            RuleFor(x => x.SupplierId)
                .Must(SharedFunctions.IsValidHexString)
                .WithMessage("O Id inserido para o fornecedor é invalido.")
                .When(x => !string.IsNullOrEmpty(x.SupplierId));
        }

        public async Task<ValidationResult> ValidateAsync(ICreateProductRequest entity)
        {

            var validationResult = await base.ValidateAsync(entity);
            return new ValidationResult(validationResult.IsValid, validationResult.Errors.Select(x => new ValidationResultErrors
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName,
            }));
        }

        public new ValidationResult Validate(ICreateProductRequest entity)
        {
            var validationResult = base.Validate(entity);

            var errors = new ValidationResult(validationResult.IsValid, validationResult.Errors.Select(x => new ValidationResultErrors
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName,
            }));

            return errors;
        }

        

    }
}
