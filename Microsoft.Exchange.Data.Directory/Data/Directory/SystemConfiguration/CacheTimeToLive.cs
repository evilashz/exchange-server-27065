using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000653 RID: 1619
	internal static class CacheTimeToLive
	{
		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x00118571 File Offset: 0x00116771
		public static TimeSpan FederatedCacheTimeToLive
		{
			get
			{
				return CacheTimeToLive.FederatedCacheTimeToLiveData.Value;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x0011857D File Offset: 0x0011677D
		public static TimeSpan OrgPropertyCacheTimeToLive
		{
			get
			{
				return CacheTimeToLive.OrgPropertyCacheTimeToLiveData.Value;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x00118589 File Offset: 0x00116789
		public static TimeSpan GlobalCountryListCacheTimeToLive
		{
			get
			{
				return CacheTimeToLive.GlobalCountryListCacheTimeToLiveData.Value;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x00118595 File Offset: 0x00116795
		private static TimeSpan DefaultFederatedCacheTimeToLive
		{
			get
			{
				if (!ExEnvironment.IsTest)
				{
					return TimeSpan.FromMinutes(5.0);
				}
				return TimeSpan.FromMilliseconds(1.0);
			}
		}

		// Token: 0x0400341C RID: 13340
		private static readonly TimeSpanAppSettingsEntry FederatedCacheTimeToLiveData = new TimeSpanAppSettingsEntry("FederatedCacheTimeToLive", TimeSpanUnit.Seconds, CacheTimeToLive.DefaultFederatedCacheTimeToLive, ExTraceGlobals.SystemConfigurationCacheTracer);

		// Token: 0x0400341D RID: 13341
		private static readonly TimeSpanAppSettingsEntry OrgPropertyCacheTimeToLiveData = new TimeSpanAppSettingsEntry("OrgPropertyCacheTimeToLiveSeconds", TimeSpanUnit.Seconds, TimeSpan.FromHours(1.0), ExTraceGlobals.SystemConfigurationCacheTracer);

		// Token: 0x0400341E RID: 13342
		private static readonly TimeSpanAppSettingsEntry GlobalCountryListCacheTimeToLiveData = new TimeSpanAppSettingsEntry("GlobalCountryListCacheTimeToLiveSeconds", TimeSpanUnit.Seconds, TimeSpan.FromHours(6.0), ExTraceGlobals.SystemConfigurationCacheTracer);
	}
}
