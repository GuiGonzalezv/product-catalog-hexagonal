namespace ProductCatalog.Domain.Ports
{
    public interface IMapper
    {
        TResult Map<TResult>(object origin);
    }
}
