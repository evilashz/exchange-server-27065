using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000038 RID: 56
	internal interface IExecutionSettings
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600028D RID: 653
		VariantConfigurationSnapshot Snapshot { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600028E RID: 654
		bool DiscoveryUseFastSearch { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600028F RID: 655
		bool DiscoveryAggregateLogs { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000290 RID: 656
		bool DiscoveryExecutesInParallel { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000291 RID: 657
		bool DiscoveryLocalSearchIsParallel { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000292 RID: 658
		int DiscoveryMaxMailboxes { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000293 RID: 659
		uint DiscoveryMaxAllowedExecutorItems { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000294 RID: 660
		int DiscoveryMaxAllowedMailboxQueriesPerRequest { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000295 RID: 661
		uint DiscoverySynchronousConcurrency { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000296 RID: 662
		uint DiscoveryADLookupConcurrency { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000297 RID: 663
		uint DiscoveryFanoutConcurrency { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000298 RID: 664
		uint DiscoveryServerLookupConcurrency { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000299 RID: 665
		uint DiscoveryLocalSearchConcurrency { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600029A RID: 666
		uint DiscoveryServerLookupBatch { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600029B RID: 667
		uint DiscoveryFanoutBatch { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600029C RID: 668
		uint DiscoveryLocalSearchBatch { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600029D RID: 669
		int DiscoveryKeywordsBatchSize { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600029E RID: 670
		uint DiscoveryDisplaySearchBatchSize { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600029F RID: 671
		uint DiscoveryDisplaySearchPageSize { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002A0 RID: 672
		int DiscoveryMaxAllowedResultsPageSize { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002A1 RID: 673
		int DiscoveryDefaultPageSize { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A2 RID: 674
		uint DiscoveryADPageSize { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002A3 RID: 675
		TimeSpan SearchTimeout { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002A4 RID: 676
		TimeSpan ServiceTopologyTimeout { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002A5 RID: 677
		TimeSpan MailboxServerLocatorTimeout { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002A6 RID: 678
		List<DefaultFolderType> ExcludedFolders { get; }
	}
}
