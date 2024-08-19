
using FluentValidation;
using ProductCatalog.Application.Services.Product.Base;

namespace ProductCatalog.Application.Services.Product.Validations
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
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
        }
    }
}
