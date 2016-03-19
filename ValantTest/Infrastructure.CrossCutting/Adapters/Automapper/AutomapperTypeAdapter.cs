namespace ValantTest.Infrastructure.CrossCutting.Adapters.Automapper
{
    using AutoMapper;

    public class AutomapperTypeAdapter
       : ITypeAdapter
    {
        private readonly IMapper mapper;

        public AutomapperTypeAdapter(MapperConfiguration config)
        {
            this.mapper = config.CreateMapper();
        }

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return this.mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return this.mapper.Map<TTarget>(source);
        }
    }
}
