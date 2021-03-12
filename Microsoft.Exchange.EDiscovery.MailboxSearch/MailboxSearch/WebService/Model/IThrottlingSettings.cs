using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000036 RID: 54
	internal interface IThrottlingSettings
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000278 RID: 632
		uint DiscoveryMaxConcurrency { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000279 RID: 633
		uint DiscoveryMaxKeywords { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600027A RID: 634
		uint DiscoveryMaxKeywordsPerPage { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600027B RID: 635
		uint DiscoveryMaxMailboxes { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600027C RID: 636
		uint DiscoveryMaxPreviewSearchMailboxes { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600027D RID: 637
		uint DiscoveryMaxRefinerResults { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600027E RID: 638
		uint DiscoveryMaxSearchQueueDepth { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600027F RID: 639
		uint DiscoveryMaxStatsSearchMailboxes { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000280 RID: 640
		uint DiscoveryPreviewSearchResultsPageSize { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000281 RID: 641
		uint DiscoverySearchTimeoutPeriod { get; }
	}
}
