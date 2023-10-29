using EPiServer.Framework.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace EmptyCommerce2.Models.Media
{
    [ContentType(
        DisplayName = "Image",
        GUID = "13a7f816-9d1b-4203-903c-431b9014b0a8"
    )]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,png")]
    public class ImageFile : ImageData
    {
        [Display(
            Name = "Alternate Text"
        )]
        public virtual string? AltText { get; set; }
    }
}
