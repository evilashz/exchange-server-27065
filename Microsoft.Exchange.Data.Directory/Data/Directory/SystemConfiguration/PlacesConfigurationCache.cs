using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000647 RID: 1607
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PlacesConfigurationCache : LazyLookupTimeoutCache<string, ServiceEndpoint>
	{
		// Token: 0x06004B94 RID: 19348 RVA: 0x0011699A File Offset: 0x00114B9A
		protected PlacesConfigurationCache() : base(1, 6, false, TimeSpan.FromDays(1.0))
		{
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06004B95 RID: 19349 RVA: 0x001169B3 File Offset: 0x00114BB3
		private string MapControlUrl
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointUrl("MapControlUrl");
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x001169C0 File Offset: 0x00114BC0
		public virtual bool IsFeatureEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.LocationServicesUrl) && !string.IsNullOrEmpty(this.LocationServicesKey) && !string.IsNullOrEmpty(this.PhonebookServicesUrl) && !string.IsNullOrEmpty(this.PhonebookServicesKey) && !string.IsNullOrEmpty(this.StaticMapUrl) && !string.IsNullOrEmpty(this.MapControlUrl) && !string.IsNullOrEmpty(this.MapControlKey) && !string.IsNullOrEmpty(this.DirectionsPageUrl);
			}
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06004B97 RID: 19351 RVA: 0x00116A38 File Offset: 0x00114C38
		public virtual string LocationServicesUrl
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointUrl("LocationServicesUrl");
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06004B98 RID: 19352 RVA: 0x00116A44 File Offset: 0x00114C44
		public virtual string LocationServicesKey
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointToken("LocationServicesUrl");
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06004B99 RID: 19353 RVA: 0x00116A50 File Offset: 0x00114C50
		public virtual string PhonebookServicesUrl
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointUrl("PhonebookServicesUrl");
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x00116A5C File Offset: 0x00114C5C
		public virtual string PhonebookServicesKey
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointToken("PhonebookServicesUrl");
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x00116A68 File Offset: 0x00114C68
		public virtual string StaticMapUrl
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointUrl("StaticMapUrl");
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x00116A74 File Offset: 0x00114C74
		public virtual string MapControlKey
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointToken("MapControlUrl");
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x00116A80 File Offset: 0x00114C80
		public virtual string DirectionsPageUrl
		{
			get
			{
				return PlacesConfigurationCache.GetEndpointUrl("DirectionsPageUrl");
			}
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00116A8C File Offset: 0x00114C8C
		protected override ServiceEndpoint CreateOnCacheMiss(string serviceEndpointId, ref bool shouldAdd)
		{
			PlacesConfigurationCache.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PlacesConfigurationCache.CreateOnCacheMiss called for service endpoint id: {0}", serviceEndpointId);
			ServiceEndpoint result = null;
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 215, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\PlacesConfigurationCache.cs");
				ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
				result = endpointContainer.GetEndpoint(serviceEndpointId);
			}
			catch (EndpointContainerNotFoundException)
			{
				PlacesConfigurationCache.Tracer.TraceDebug(0L, "PlacesConfigurationCache: Endpoint Container doesn't exist.");
			}
			catch (ServiceEndpointNotFoundException)
			{
				PlacesConfigurationCache.Tracer.TraceDebug<string>(0L, "PlacesConfigurationCache: Endpoint '{0}' doesn't exist.", serviceEndpointId);
			}
			catch (LocalizedException arg)
			{
				PlacesConfigurationCache.Tracer.TraceError<LocalizedException>(0L, "PlacesConfigurationCache: Unable to read service endpoint due to exception: {0}", arg);
			}
			return result;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x00116B4C File Offset: 0x00114D4C
		private static string GetEndpointUrl(string serviceEndpointId)
		{
			ServiceEndpoint serviceEndpoint = PlacesConfigurationCache.Instance.Get(serviceEndpointId);
			if (serviceEndpoint != null && serviceEndpoint.Uri != null)
			{
				return serviceEndpoint.Uri.OriginalString;
			}
			return null;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00116B84 File Offset: 0x00114D84
		private static string GetEndpointToken(string serviceEndpointId)
		{
			ServiceEndpoint serviceEndpoint = PlacesConfigurationCache.Instance.Get(serviceEndpointId);
			if (serviceEndpoint != null)
			{
				return serviceEndpoint.Token;
			}
			return null;
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00116BA8 File Offset: 0x00114DA8
		public static bool IsRestrictedCulture(string culture)
		{
			return PlacesConfigurationCache.RestrictedCultures.Contains(culture);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00116BB8 File Offset: 0x00114DB8
		public static string GetMapControlUrl(string culture)
		{
			if (PlacesConfigurationCache.Instance.IsFeatureEnabled && !PlacesConfigurationCache.IsRestrictedCulture(culture))
			{
				StringBuilder stringBuilder = new StringBuilder(PlacesConfigurationCache.Instance.MapControlUrl);
				stringBuilder.AppendFormat("&mkt={0}", culture);
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x040033E9 RID: 13289
		private const string MarketParameter = "&mkt={0}";

		// Token: 0x040033EA RID: 13290
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x040033EB RID: 13291
		private static readonly HashSet<string> RestrictedCultures = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"hy-AM",
			"az-Cyrl-AZ",
			"az-Latn-AZ",
			"es-AR",
			"zh-CN",
			"zh-HK",
			"zh-TW",
			"zh-MO",
			"mn-Mong-CN",
			"bo-CN",
			"ug-CN",
			"ii-CN",
			"as-IN",
			"bn-IN",
			"gu-IN",
			"hi-IN",
			"kn-IN",
			"kok-IN",
			"ml-IN",
			"mr-IN",
			"or-IN",
			"pa-IN",
			"sa-IN",
			"ta-IN",
			"te-IN",
			"he-IL",
			"ko-KR",
			"ar-MA",
			"pa-Arab-PK",
			"sd-Arab-PK",
			"ur-PK",
			"sr-Cyrl-RS",
			"sr-Latn-RS",
			"sr-Cyrl-CS",
			"sr-Latn-CS",
			"tr-TR",
			"es-VE"
		};

		// Token: 0x040033EC RID: 13292
		public static readonly PlacesConfigurationCache Instance = new PlacesConfigurationCache();
	}
}
