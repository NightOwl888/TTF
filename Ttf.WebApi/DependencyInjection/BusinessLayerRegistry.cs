using StructureMap;
using Ttf.BusinessLayer.Foo;

namespace Ttf.WebApi.DependencyInjection
{
    /// <summary>
    /// Registry for dependency injection configuration of business layer components.
    /// </summary>
    internal class BusinessLayerRegistry : Registry
    {
        public BusinessLayerRegistry()
        {
            this.Scan(x =>
            {
                x.AssemblyContainingType<IFooCalculator>();
                x.WithDefaultConventions();
                x.AddAllTypesOf<IFooCalculator>();
            });
        }
    }
}