using System.Collections.Concurrent;

namespace EmptyCommerce2.Features.Settings
{
    public interface ISettingsService
    {
        //ContentReference GlobalSettingsRoot { get; set; }
        //ConcurrentDictionary<string, Dictionary<Type, object>> SiteSettings { get; }
        T GetSiteSettings<T>(Guid? siteId = null);
        //void InitializeSettings();
        //void UnintializeSettings();
        //void UpdateSettings(Guid siteId, IContent content, bool isContentNotPublished);
        //void UpdateSettings();
    }
}
