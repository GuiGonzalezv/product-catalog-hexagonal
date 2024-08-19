using AutoMapper;

namespace ProductCatalog.Infra.Mapper
{
    public class Mapper : Domain.Ports.IMapper
    {
        private readonly IMapper _mapper;
        public Mapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TResult Map<TResult>(object origin)
        {
            return _mapper.Map<TResult>(origin);
        }

    }
}