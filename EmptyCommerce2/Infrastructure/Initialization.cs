using EPiServer.Framework.Initialization;
using EPiServer.Framework;
using EPiServer.Commerce.Routing;

namespace EmptyCommerce2.Infrastructure
{
    [InitializableModule]
    public class Initialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //CatalogRouteHelper.MapDefaultHierarchialRouter(false);
        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
