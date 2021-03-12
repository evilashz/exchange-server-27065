using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000649 RID: 1609
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WeatherConfigurationCache : LazyLookupTimeoutCache<string, ServiceEndpoint>, IWeatherConfigurationCache
	{
		// Token: 0x06004BA7 RID: 19367 RVA: 0x00116DF2 File Offset: 0x00114FF2
		private WeatherConfigurationCache() : base(1, 6, false, TimeSpan.FromDays(1.0))
		{
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x00116E0B File Offset: 0x0011500B
		public bool IsFeatureEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.WeatherServiceUrl);
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x00116E1B File Offset: 0x0011501B
		public string WeatherServiceUrl
		{
			get
			{
				return WeatherConfigurationCache.GetEndpointUrl("WeatherServicesUrl");
			}
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00116E27 File Offset: 0x00115027
		public bool IsRestrictedCulture(string culture)
		{
			return !WeatherConfigurationCache.SupportedCultures.Contains(culture);
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x00116E88 File Offset: 0x00115088
		protected override ServiceEndpoint CreateOnCacheMiss(string serviceEndpointId, ref bool shouldAdd)
		{
			WeatherConfigurationCache.Tracer.TraceDebug<string>((long)this.GetHashCode(), "WeatherConfigurationCache.CreateOnCacheMiss called for service endpoint id: {0}", serviceEndpointId);
			ServiceEndpoint endpoint = null;
			try
			{
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 259, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\WeatherConfigurationCache.cs");
					ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
					endpoint = endpointContainer.GetEndpoint(serviceEndpointId);
				});
			}
			catch (EndpointContainerNotFoundException)
			{
				WeatherConfigurationCache.Tracer.TraceDebug(0L, "WeatherConfigurationCache: Endpoint Container doesn't exist.");
			}
			catch (ServiceEndpointNotFoundException)
			{
				WeatherConfigurationCache.Tracer.TraceDebug<string>(0L, "WeatherConfigurationCache: Endpoint '{0}' doesn't exist.", serviceEndpointId);
			}
			catch (LocalizedException arg)
			{
				WeatherConfigurationCache.Tracer.TraceError<LocalizedException>(0L, "WeatherConfigurationCache: Unable to read service endpoint due to exception: {0}", arg);
			}
			return endpoint;
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00116F54 File Offset: 0x00115154
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static bool IsWeatherEnabledByDefault()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00116F78 File Offset: 0x00115178
		private static string GetEndpointUrl(string serviceEndpointId)
		{
			ServiceEndpoint serviceEndpoint = WeatherConfigurationCache.Instance.Get(serviceEndpointId);
			if (serviceEndpoint != null && serviceEndpoint.Uri != null)
			{
				return serviceEndpoint.Uri.OriginalString;
			}
			if (WeatherConfigurationCache.IsWeatherEnabledByDefault())
			{
				return "http://api.weather.msn.com/data.aspx";
			}
			return null;
		}

		// Token: 0x040033ED RID: 13293
		private const string WeatherServiceUrlDefault = "http://api.weather.msn.com/data.aspx";

		// Token: 0x040033EE RID: 13294
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x040033EF RID: 13295
		public static readonly WeatherConfigurationCache Instance = new WeatherConfigurationCache();

		// Token: 0x040033F0 RID: 13296
		private static readonly HashSet<string> SupportedCultures = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"af-ZA",
			"am-ET",
			"ar-AE",
			"ar-BH",
			"ar-DZ",
			"ar-EG",
			"ar-IQ",
			"ar-JO",
			"ar-KW",
			"ar-LB",
			"ar-LY",
			"ar-MA",
			"ar-OM",
			"ar-QA",
			"ar-SA",
			"ar-SY",
			"ar-TN",
			"ar-XA",
			"ar-YE",
			"as-IN",
			"az-Latn-AZ",
			"bg-BG",
			"bn-BD",
			"bn-IN",
			"bs-Cyrl-BA",
			"bs-Latn-BA",
			"ca-ES",
			"cs-CZ",
			"cy-GB",
			"da-DK",
			"de-AT",
			"de-CH",
			"de-DE",
			"el-GR",
			"en-AE",
			"en-AU",
			"en-CA",
			"en-GB",
			"en-HK",
			"en-ID",
			"en-IE",
			"en-IN",
			"en-MY",
			"en-NZ",
			"en-PH",
			"en-SG",
			"en-US",
			"en-VI",
			"en-VN",
			"en-XA",
			"en-XU",
			"en-ZA",
			"es-AR",
			"es-CL",
			"es-CO",
			"es-ES",
			"es-MX",
			"es-PE",
			"es-US",
			"es-VE",
			"es-XL",
			"et-EE",
			"eu-ES",
			"fa-IR",
			"fi-FI",
			"fil-PH",
			"fr-BE",
			"fr-CA",
			"fr-CH",
			"fr-FR",
			"fr-MA",
			"ga-IE",
			"gl-ES",
			"gu-IN",
			"ha-Latn-NG",
			"he-IL",
			"hi-IN",
			"hr-HR",
			"hu-HU",
			"hy-AM",
			"id-ID",
			"ig-NG",
			"is-IS",
			"it-IT",
			"iu-Latn-CA",
			"ja-JP",
			"ka-GE",
			"kk-KZ",
			"km-KH",
			"kn-IN",
			"kok-IN",
			"ko-KR",
			"ky-KG",
			"lb-LU",
			"lo-LA",
			"lt-LT",
			"lv-LV",
			"mi-NZ",
			"mk-MK",
			"ml-IN",
			"mn-MN",
			"mr-IN",
			"ms-BN",
			"ms-MY",
			"mt-MT",
			"nb-NO",
			"ne-NP",
			"nl-BE",
			"nl-NL",
			"nn-NO",
			"nso-ZA",
			"or-IN",
			"pa-IN",
			"pl-PL",
			"ps-AF",
			"ps-PS",
			"pt-BR",
			"pt-PT",
			"quz-PE",
			"ro-RO",
			"ru-RU",
			"rw-RW",
			"si-LK",
			"sk-SK",
			"sl-SI",
			"sq-AL",
			"sr-Cyrl-RS",
			"sr-Latn-RS",
			"sv-SE",
			"sw-KE",
			"ta-IN",
			"te-IN",
			"th-TH",
			"tk-TM",
			"tn-ZA",
			"tr-TR",
			"tt-RU",
			"uk-UA",
			"ur-PK",
			"uz-Latn-UZ",
			"vi-VN",
			"wo-SN",
			"xh-ZA",
			"yo-NG",
			"zh-HK",
			"zh-SG",
			"zu-ZA",
			"az-Latn",
			"bs-Cyrl",
			"bs-Latn",
			"ha-Latn",
			"iu-Latn",
			"sr-Cyrl",
			"sr-Latn",
			"uz-Latn"
		};
	}
}
