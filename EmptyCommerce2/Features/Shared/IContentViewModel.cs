using EmptyCommerce2.Models.Pages;
using Microsoft.AspNetCore.Html;

namespace EmptyCommerce2.Features.Shared
{
    public interface IContentViewModel<out TContent> where TContent : IContent
    {
        TContent CurrentContent { get; }
        HomePage StartPage { get; }
        //HtmlString SchemaMarkup { get; }
    }
}
