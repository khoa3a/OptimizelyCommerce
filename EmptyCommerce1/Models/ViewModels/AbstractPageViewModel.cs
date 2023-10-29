namespace EmptyCommerce1.Models.ViewModels
{
    public abstract class AbstractPageViewModel
    {
        public string? PageTitle { get; set; }
        public string? PageHeader { get; set; }
    }

    public class AbstractPageViewModel<T> : AbstractPageViewModel
    {
        public AbstractPageViewModel(T page)
        {
            Page = page;
        }

        public T Page { get; }
    }
}
