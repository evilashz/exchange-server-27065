using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.CommonHelpProvider
{
	// Token: 0x02000007 RID: 7
	internal sealed class HelpProviderCache : LazyLookupTimeoutCache<OrganizationId, HelpProviderCache.Item>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000033BF File Offset: 0x000015BF
		private HelpProviderCache() : base(HelpProviderCache.HelpProviderCacheBuckets.Value, HelpProviderCache.HelpProviderCacheBucketSize.Value, false, HelpProviderCache.HelpProviderCacheTimeToExpire.Value, HelpProviderCache.HelpProviderCacheTimeToLive.Value)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000033F0 File Offset: 0x000015F0
		internal static HelpProviderCache Instance
		{
			get
			{
				return HelpProviderCache.instance;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000033F8 File Offset: 0x000015F8
		protected override HelpProviderCache.Item CreateOnCacheMiss(OrganizationId key, ref bool shouldAdd)
		{
			shouldAdd = true;
			ExchangeAssistance exchangeAssistanceObjectFromAD = HelpProvider.GetExchangeAssistanceObjectFromAD(key);
			Uri privacyStatementUrl = null;
			Uri communityUrl = null;
			bool? privacyLinkDisplayEnabled = null;
			if (exchangeAssistanceObjectFromAD != null)
			{
				if (exchangeAssistanceObjectFromAD.CommunityLinkDisplayEnabled)
				{
					communityUrl = exchangeAssistanceObjectFromAD.CommunityURL;
				}
				privacyLinkDisplayEnabled = new bool?(exchangeAssistanceObjectFromAD.PrivacyLinkDisplayEnabled);
				if (exchangeAssistanceObjectFromAD.PrivacyLinkDisplayEnabled)
				{
					privacyStatementUrl = exchangeAssistanceObjectFromAD.PrivacyStatementURL;
				}
			}
			return new HelpProviderCache.Item(privacyStatementUrl, communityUrl, privacyLinkDisplayEnabled);
		}

		// Token: 0x0400004A RID: 74
		private static readonly IntAppSettingsEntry HelpProviderCacheBucketSize = new IntAppSettingsEntry("HelpProviderCacheBucketSize", 1024, ExTraceGlobals.CoreTracer);

		// Token: 0x0400004B RID: 75
		private static readonly IntAppSettingsEntry HelpProviderCacheBuckets = new IntAppSettingsEntry("HelpProviderCacheBuckets", 5, ExTraceGlobals.CoreTracer);

		// Token: 0x0400004C RID: 76
		private static readonly TimeSpanAppSettingsEntry HelpProviderCacheTimeToExpire = new TimeSpanAppSettingsEntry("HelpProviderCacheTimeToExpire", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(15.0), ExTraceGlobals.CoreTracer);

		// Token: 0x0400004D RID: 77
		private static readonly TimeSpanAppSettingsEntry HelpProviderCacheTimeToLive = new TimeSpanAppSettingsEntry("HelpProviderCacheTimeToLive", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(60.0), ExTraceGlobals.CoreTracer);

		// Token: 0x0400004E RID: 78
		private static HelpProviderCache instance = new HelpProviderCache();

		// Token: 0x02000008 RID: 8
		internal sealed class Item
		{
			// Token: 0x06000044 RID: 68 RVA: 0x000034DF File Offset: 0x000016DF
			public Item(Uri privacyStatementUrl, Uri communityUrl, bool? privacyLinkDisplayEnabled)
			{
				this.PrivacyStatementUrl = privacyStatementUrl;
				this.CommunityUrl = communityUrl;
				this.PrivacyLinkDisplayEnabled = privacyLinkDisplayEnabled;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000045 RID: 69 RVA: 0x000034FC File Offset: 0x000016FC
			// (set) Token: 0x06000046 RID: 70 RVA: 0x00003504 File Offset: 0x00001704
			public Uri CommunityUrl { get; private set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000047 RID: 71 RVA: 0x0000350D File Offset: 0x0000170D
			// (set) Token: 0x06000048 RID: 72 RVA: 0x00003515 File Offset: 0x00001715
			public Uri PrivacyStatementUrl { get; private set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000049 RID: 73 RVA: 0x0000351E File Offset: 0x0000171E
			// (set) Token: 0x0600004A RID: 74 RVA: 0x00003526 File Offset: 0x00001726
			public bool? PrivacyLinkDisplayEnabled { get; private set; }
		}
	}
}
