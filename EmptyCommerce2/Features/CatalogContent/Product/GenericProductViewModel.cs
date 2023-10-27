using EmptyCommerce2.Features.CatalogContent.Variant;

namespace EmptyCommerce2.Features.CatalogContent.Product
{
    public class GenericProductViewModel : ProductViewModelBase<GenericProduct, GenericVariant>//, IEntryViewModelBase
    {
        public GenericProductViewModel()
        {
        }

        public GenericProductViewModel(GenericProduct fashionProduct) : base(fashionProduct)
        {
        }

        //public ReviewsViewModel Reviews { get; set; }
        //public IEnumerable<Recommendation> AlternativeProducts { get; set; }
        //public IEnumerable<Recommendation> CrossSellProducts { get; set; }
    }
}
