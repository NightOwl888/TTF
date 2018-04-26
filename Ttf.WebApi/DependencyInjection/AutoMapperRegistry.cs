using AutoMapper;
using StructureMap;
using Ttf.BusinessLayer.Foo.Dto;
using Ttf.WebApi.Models;

namespace Ttf.WebApi.DependencyInjection
{
    /// <summary>
    /// Registry for dependency injection configuration of AutoMapper.
    /// </summary>
    internal class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            // NOTE: In a production application, it would probably
            // be better to put some time into making this convention-based.
            // But for this simple demonstration, I opted to skip this step.
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FooRequest, FooCalculatorRequest>();
                cfg.CreateMap<FooCalculatorResult, FooResult>();
            });


            IMapper mapper = config.CreateMapper();

            this.For<IMapper>().Use(mapper);
        }
    }
}