
using FluentValidation;
using ProductCatalog.Domain.Dtos.Product;
using ProductCatalog.Domain.Ports.Validation;
using ProductCatalog.Infra.FluentValidation.Product;

namespace ProductCatalog.Application.Services.Product.Validations
{
    public class UpdateProductRequestValidator : AbstractValidator<IUpdateProductRequest>, Domain.Ports.Validation.IValidator<IUpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id do produto é obrigatório.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .Length(3, 100).WithMessage("O nome do produto deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("A descrição não pode ter mais que 500 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");

            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("O Id do fornecedor é obrigatório.");

            RuleFor(x => x.SupplierId)
                .Must(SharedFunctions.IsValidHexString)
                .WithMessage("O Id inserido para o fornecedor é invalido.")
                .When(x => !string.IsNullOrEmpty(x.SupplierId));

            RuleFor(x => x.Id)
                .Must(SharedFunctions.IsValidHexString)
                .WithMessage("O Id do produto é invalido")
                .When(x => !string.IsNullOrEmpty(x.Id));
        }
        public async Task<ValidationResult> ValidateAsync(IUpdateProductRequest entity)
        {
            var validationResult = await base.ValidateAsync(entity);
            return new ValidationResult(validationResult.IsValid, validationResult.Errors.Select(x => new ValidationResultErrors
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName,
            }));
        }

        public ValidationResult Validate(IUpdateProductRequest entity)
        {
            var validationResult = base.Validate(entity);

            return new ValidationResult(validationResult.IsValid, validationResult.Errors.Select(x => new ValidationResultErrors
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName,
            }));
        }
    }
}
