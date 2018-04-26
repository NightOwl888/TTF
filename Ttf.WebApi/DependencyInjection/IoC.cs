using StructureMap;

namespace Ttf.WebApi.DependencyInjection
{
    /// <summary>
    /// Bootstrapper to configure the dependency injection container.
    /// Note that StructureMap automatically registers controllers so there
    /// is no need to do so explicitly.
    /// </summary>
    internal static class IoC
    {
        public static IContainer Container { get; private set; }

        public static IContainer Register()
        {
            Container = new Container(c =>
            {
                c.AddRegistry<AutoMapperRegistry>();
                c.AddRegistry<BusinessLayerRegistry>();
            });

            return Container;
        }
    }
}