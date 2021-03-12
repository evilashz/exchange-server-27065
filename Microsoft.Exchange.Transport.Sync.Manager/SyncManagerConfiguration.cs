using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncManagerConfiguration : ISyncManagerConfiguration, ISubscriptionProcessPermitterConfig
	{
		// Token: 0x0600034B RID: 843 RVA: 0x00016617 File Offset: 0x00014817
		private SyncManagerConfiguration()
		{
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001661F File Offset: 0x0001481F
		public static SyncManagerConfiguration Instance
		{
			get
			{
				return SyncManagerConfiguration.instance;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00016626 File Offset: 0x00014826
		public TimeSpan WorkTypeBudgetManagerSlidingWindowLength
		{
			get
			{
				return ContentAggregationConfig.WorkTypeBudgetManagerSlidingWindowLength;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001662D File Offset: 0x0001482D
		public TimeSpan WorkTypeBudgetManagerSlidingBucketLength
		{
			get
			{
				return ContentAggregationConfig.WorkTypeBudgetManagerSlidingBucketLength;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00016634 File Offset: 0x00014834
		public TimeSpan WorkTypeBudgetManagerSampleDispatchedWorkFrequency
		{
			get
			{
				return ContentAggregationConfig.WorkTypeBudgetManagerSampleDispatchedWorkFrequency;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0001663B File Offset: 0x0001483B
		public TimeSpan DatabaseBackoffTime
		{
			get
			{
				return ContentAggregationConfig.DatabaseBackoffTime;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00016642 File Offset: 0x00014842
		public int MaxSyncsPerDB
		{
			get
			{
				return ContentAggregationConfig.MaxSyncsPerDB;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00016649 File Offset: 0x00014849
		public TimeSpan DispatchEntryExpirationTime
		{
			get
			{
				return ContentAggregationConfig.DispatchEntryExpirationTime;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00016650 File Offset: 0x00014850
		public TimeSpan DispatchEntryExpirationCheckFrequency
		{
			get
			{
				return ContentAggregationConfig.DispatchEntryExpirationCheckFrequency;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00016657 File Offset: 0x00014857
		public TimeSpan DispatcherDatabaseRefreshFrequency
		{
			get
			{
				return ContentAggregationConfig.DispatcherDatabaseRefreshFrequency;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0001665E File Offset: 0x0001485E
		public bool AggregationSubscriptionsEnabled
		{
			get
			{
				return ContentAggregationConfig.AggregationSubscriptionsEnabled;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00016665 File Offset: 0x00014865
		public bool MigrationSubscriptionsEnabled
		{
			get
			{
				return ContentAggregationConfig.MigrationSubscriptionsEnabled;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0001666C File Offset: 0x0001486C
		public bool PeopleConnectionSubscriptionsEnabled
		{
			get
			{
				return ContentAggregationConfig.PeopleConnectionSubscriptionsEnabled;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00016673 File Offset: 0x00014873
		public bool PopAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.PopAggregationEnabled;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001667A File Offset: 0x0001487A
		public bool DeltaSyncAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.DeltaSyncAggregationEnabled;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00016681 File Offset: 0x00014881
		public bool ImapAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.ImapAggregationEnabled;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00016688 File Offset: 0x00014888
		public bool FacebookAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.FacebookAggregationEnabled;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001668F File Offset: 0x0001488F
		public bool LinkedInAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.LinkedInAggregationEnabled;
			}
		}

		// Token: 0x040001DB RID: 475
		private static SyncManagerConfiguration instance = new SyncManagerConfiguration();
	}
}
