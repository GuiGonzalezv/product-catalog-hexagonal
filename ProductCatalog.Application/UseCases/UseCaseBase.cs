using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Validation;

namespace ProductCatalog.Application.UseCases
{
    public abstract class UseCaseBase
    {
        protected UseCaseBase(IOutputPort outputPort, IMapper mapper)
        {
            OutputPort = outputPort;
            Mapper = mapper;
        }

        protected IOutputPort OutputPort { get; }

        protected IMapper Mapper { get; }

        protected void Handle<TResponse>(string error) where TResponse : class
        {
            OutputPort.Handle(new UseCaseOutput<TResponse>(new List<ErrorResponse> { new(error) }));
        }

        protected void Handle<TResponse>(IEnumerable<string> errors) where TResponse : class
        {
            OutputPort.Handle(new UseCaseOutput<TResponse>(errors.Select(s => new ErrorResponse(s))));
        }

        protected void Handle<TResponse>(object model) where TResponse : class
        {
            var response = Mapper.Map<TResponse>(model);
            OutputPort.Handle(new UseCaseOutput<TResponse>(response));
        }

        protected void Handle<TResponse>(TResponse model) where TResponse : class
        {
            OutputPort.Handle(new UseCaseOutput<TResponse>(model));
        }

        protected bool IsValid<TRequest, TValidator>(TValidator validator, TRequest request)
            where TRequest : class
            where TValidator : IValidator<TRequest>
        {
            if (request is null)
            {
                Handle<ErrorResponse>("Objeto inválido.");
                return false;
            }

            var validation = validator.Validate(request);

            if (validation.IsValid)
                return true;

            Handle<ErrorResponse>(validation.Errors.Select(x => x.ErrorMessage));
            return false;
        }

        protected async Task<bool> IsValidAsync<TRequest, TValidator>(TValidator validator, TRequest request)
            where TRequest : class
            where TValidator : IValidator<TRequest>
        {
            if (request is null)
            {
                Handle<ErrorResponse>("Objeto inválido.");
                return false;
            }

            var validation = await validator.ValidateAsync(request);

            if (validation.IsValid)
                return true;

            Handle<ErrorResponse>(validation.Errors.Select(x => x.ErrorMessage));
            return false;
        }
    }
}
