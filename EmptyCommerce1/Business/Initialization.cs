using EPiServer.Framework.Initialization;
using EPiServer.Framework;
using EPiServer.Commerce.Routing;

namespace EmptyCommerce1.Business
{
    [InitializableModule]
    public class Initialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //CatalogRouteHelper.MapDefaultHierarchialRouter(false);
        }        

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
