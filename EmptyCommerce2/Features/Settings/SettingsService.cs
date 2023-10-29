using EPiServer.Framework.TypeScanner;
using EPiServer.Globalization;
using EPiServer.Web;
using System.Collections.Concurrent;

namespace EmptyCommerce2.Features.Settings
{
    public class SettingsService : ISettingsService
    {
        public const string GlobalSettingsRootName = "Global Settings Root";
        private readonly IContentRepository _contentRepository;
        private readonly IContentVersionRepository _contentVersionRepository;
        private readonly ContentRootService _contentRootService;
        private readonly IContentTypeRepository _contentTypeRepository;
        //private readonly ILogger _log = LogManager.GetLogger();
        private readonly ITypeScannerLookup _typeScannerLookup;
        private readonly IContentEvents _contentEvents;
        private readonly ISiteDefinitionEvents _siteDefinitionEvents;
        private readonly ISiteDefinitionRepository _siteDefinitionRepository;
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContextModeResolver _contextModeResolver;

        public SettingsService(
            IContentRepository contentRepository,
            IContentVersionRepository contentVersionRepository,
            ContentRootService contentRootService,
            ITypeScannerLookup typeScannerLookup,
            IContentTypeRepository contentTypeRepository,
            IContentEvents contentEvents,
            ISiteDefinitionEvents siteDefinitionEvents,
            ISiteDefinitionRepository siteDefinitionRepository,
            ISiteDefinitionResolver siteDefinitionResolver,
            IHttpContextAccessor httpContextAccessor,
            IContextModeResolver contextModeResolver)
        {
            _contentRepository = contentRepository;
            _contentVersionRepository = contentVersionRepository;
            _contentRootService = contentRootService;
            _typeScannerLookup = typeScannerLookup;
            _contentTypeRepository = contentTypeRepository;
            _contentEvents = contentEvents;
            _siteDefinitionEvents = siteDefinitionEvents;
            _siteDefinitionRepository = siteDefinitionRepository;
            _siteDefinitionResolver = siteDefinitionResolver;
            _httpContextAccessor = httpContextAccessor;
            _contextModeResolver = contextModeResolver;
        }
        public ConcurrentDictionary<string, Dictionary<Type, object>> SiteSettings { get; } = new ConcurrentDictionary<string, Dictionary<Type, object>>();

        public ContentReference GlobalSettingsRoot { get; set; }

        public T GetSiteSettings<T>(Guid? siteId = null)
        {
            var contentLanguage = ContentLanguage.PreferredCulture.Name;
            if (!siteId.HasValue)
            {
                siteId = ResolveSiteId();
                if (siteId == Guid.Empty)
                {
                    return default;
                }
            }
            try
            {
                if (_contextModeResolver.CurrentMode == ContextMode.Edit)
                {
                    if (SiteSettings.TryGetValue(siteId.Value.ToString() + $"-common-draft-{contentLanguage}", out var siteSettings))
                    {
                        if (siteSettings.TryGetValue(typeof(T), out var setting))
                        {
                            return (T)setting;
                        }
                    }
                    if (SiteSettings.TryGetValue(siteId.Value.ToString() + "-common-draft-default", out var defaultSiteSettings))
                    {
                        if (defaultSiteSettings.TryGetValue(typeof(T), out var defaultSetting))
                        {
                            return (T)defaultSetting;
                        }
                    }
                }
                else
                {
                    if (SiteSettings.TryGetValue(siteId.Value.ToString() + $"-{contentLanguage}", out var siteSettings) && siteSettings.TryGetValue(typeof(T), out var setting))
                    {
                        return (T)setting;
                    }
                    if (SiteSettings.TryGetValue(siteId.Value.ToString() + "-default", out var defaultSiteSettings) && defaultSiteSettings.TryGetValue(typeof(T), out var defaultSetting))
                    {
                        return (T)defaultSetting;
                    }
                }
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                //_log.Error($"[Settings] {keyNotFoundException.Message}", exception: keyNotFoundException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                //_log.Error($"[Settings] {argumentNullException.Message}", exception: argumentNullException);
            }

            return default;
        }

        private Guid ResolveSiteId()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return Guid.Empty;
            }
            var site = _siteDefinitionResolver.GetByHostname(request.Host.Host, true, out var hostname);
            if (site == null)
            {
                return Guid.Empty;
            }
            return site.Id;
        }
    }
}
