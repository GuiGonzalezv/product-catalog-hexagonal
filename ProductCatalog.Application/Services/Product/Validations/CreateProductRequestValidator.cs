
using FluentValidation;
using ProductCatalog.Application.Services.Product.Base;

namespace ProductCatalog.Application.Services.Product.Validations
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("É necessário informar o nome do produto.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("É necessário informar a descrição do produto.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Preço do produto tem que ser maior que 0.");
            RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage("É necessário ter estoque do produto.");
            RuleFor(x => x.SupplierId).NotEmpty().WithMessage("É necessário informar o fornecedor do produto");
        }
    }
}
