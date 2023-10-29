namespace EmptyCommerce2.Features.Settings
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class SettingsContentTypeAttribute : ContentTypeAttribute
    {
        public string SettingsName { get; set; }
    }
}
