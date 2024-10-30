namespace ProductCatalog.Domain.Ports
{
    public interface IOutputPort
    {
        void Handle<T>(UseCaseOutput<T> output) where T : class;
    }
}
