using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200029E RID: 670
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StorageLimits : IStorageLimits
	{
		// Token: 0x06001BBE RID: 7102 RVA: 0x00080815 File Offset: 0x0007EA15
		private StorageLimits()
		{
			this.storageLimitDefaults = new StorageLimits.StorageLimitDefaults();
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00080828 File Offset: 0x0007EA28
		public static StorageLimits Instance
		{
			get
			{
				if (StorageLimits.instance == null)
				{
					StorageLimits.instance = new StorageLimits();
				}
				return StorageLimits.instance;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00080840 File Offset: 0x0007EA40
		public int NamedPropertyNameMaximumLength
		{
			get
			{
				return this.CurrentLimits.NamedPropertyNameMaximumLength;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0008084D File Offset: 0x0007EA4D
		public int UserConfigurationMaxSearched
		{
			get
			{
				return this.CurrentLimits.UserConfigurationMaxSearched;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0008085A File Offset: 0x0007EA5A
		public int FindNamesViewResultsLimit
		{
			get
			{
				return this.CurrentLimits.FindNamesViewResultsLimit;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00080867 File Offset: 0x0007EA67
		public int AmbiguousNamesViewResultsLimit
		{
			get
			{
				return this.CurrentLimits.AmbiguousNamesViewResultsLimit;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00080874 File Offset: 0x0007EA74
		public int CalendarSingleInstanceLimit
		{
			get
			{
				return this.CurrentLimits.CalendarSingleInstanceLimit;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00080881 File Offset: 0x0007EA81
		public int CalendarExpansionInstanceLimit
		{
			get
			{
				return this.CurrentLimits.CalendarExpansionInstanceLimit;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0008088E File Offset: 0x0007EA8E
		public int CalendarExpansionMaxMasters
		{
			get
			{
				return this.CurrentLimits.CalendarExpansionMaxMasters;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0008089B File Offset: 0x0007EA9B
		public int CalendarMaxNumberVEventsForICalImport
		{
			get
			{
				return this.CurrentLimits.CalendarMaxNumberVEventsForICalImport;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000808A8 File Offset: 0x0007EAA8
		public int CalendarMaxNumberBytesForICalImport
		{
			get
			{
				return this.CurrentLimits.CalendarMaxNumberBytesForICalImport;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000808B5 File Offset: 0x0007EAB5
		public int RecurrenceMaximumInterval
		{
			get
			{
				return this.CurrentLimits.RecurrenceMaximumInterval;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000808C2 File Offset: 0x0007EAC2
		public int RecurrenceMaximumNumberedOccurrences
		{
			get
			{
				return this.CurrentLimits.RecurrenceMaximumNumberedOccurrences;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x000808CF File Offset: 0x0007EACF
		public int DistributionListMaxMembersPropertySize
		{
			get
			{
				return this.CurrentLimits.DistributionListMaxMembersPropertySize;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x000808DC File Offset: 0x0007EADC
		public int DistributionListMaxNumberOfEntries
		{
			get
			{
				return this.CurrentLimits.DistributionListMaxNumberOfEntries;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x000808E9 File Offset: 0x0007EAE9
		public int DefaultFolderMinimumSuffix
		{
			get
			{
				return this.CurrentLimits.DefaultFolderMinimumSuffix;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x000808F6 File Offset: 0x0007EAF6
		public int DefaultFolderMaximumSuffix
		{
			get
			{
				return this.CurrentLimits.DefaultFolderMaximumSuffix;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00080903 File Offset: 0x0007EB03
		public int DefaultFolderDataCacheMaxRowCount
		{
			get
			{
				return this.CurrentLimits.DefaultFolderDataCacheMaxRowCount;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00080910 File Offset: 0x0007EB10
		public int NotificationsMaxSubscriptions
		{
			get
			{
				return this.CurrentLimits.NotificationsMaxSubscriptions;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0008091D File Offset: 0x0007EB1D
		public int MaxDelegates
		{
			get
			{
				return this.CurrentLimits.MaxDelegates;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0008092A File Offset: 0x0007EB2A
		public BufferPoolCollection.BufferSize PropertyStreamPageSize
		{
			get
			{
				return this.CurrentLimits.PropertyStreamPageSize;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00080937 File Offset: 0x0007EB37
		public long ConversionsFolderMaxTotalMessageSize
		{
			get
			{
				return this.CurrentLimits.ConversionsFolderMaxTotalMessageSize;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00080944 File Offset: 0x0007EB44
		private IStorageLimits CurrentLimits
		{
			get
			{
				return this.testLimits ?? this.storageLimitDefaults;
			}
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00080956 File Offset: 0x0007EB56
		internal void TestSetLimits(IStorageLimits testLimits)
		{
			this.testLimits = testLimits;
		}

		// Token: 0x04001342 RID: 4930
		private readonly StorageLimits.StorageLimitDefaults storageLimitDefaults;

		// Token: 0x04001343 RID: 4931
		private static StorageLimits instance;

		// Token: 0x04001344 RID: 4932
		private IStorageLimits testLimits;

		// Token: 0x0200029F RID: 671
		private class StorageLimitDefaults : IStorageLimits
		{
			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0008095F File Offset: 0x0007EB5F
			public int NamedPropertyNameMaximumLength
			{
				get
				{
					return 127;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00080963 File Offset: 0x0007EB63
			public int UserConfigurationMaxSearched
			{
				get
				{
					return 10000;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0008096A File Offset: 0x0007EB6A
			public int FindNamesViewResultsLimit
			{
				get
				{
					return 10000;
				}
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00080971 File Offset: 0x0007EB71
			public int AmbiguousNamesViewResultsLimit
			{
				get
				{
					return 200;
				}
			}

			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00080978 File Offset: 0x0007EB78
			public int CalendarSingleInstanceLimit
			{
				get
				{
					return 10000;
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0008097F File Offset: 0x0007EB7F
			public int CalendarExpansionInstanceLimit
			{
				get
				{
					return 5000;
				}
			}

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00080986 File Offset: 0x0007EB86
			public int CalendarExpansionMaxMasters
			{
				get
				{
					return 1000;
				}
			}

			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0008098D File Offset: 0x0007EB8D
			public int CalendarMaxNumberVEventsForICalImport
			{
				get
				{
					return 10000;
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00080994 File Offset: 0x0007EB94
			public int CalendarMaxNumberBytesForICalImport
			{
				get
				{
					return 10485760;
				}
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0008099B File Offset: 0x0007EB9B
			public int RecurrenceMaximumInterval
			{
				get
				{
					return 1000;
				}
			}

			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x000809A2 File Offset: 0x0007EBA2
			public int RecurrenceMaximumNumberedOccurrences
			{
				get
				{
					return 999;
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000809A9 File Offset: 0x0007EBA9
			public int DistributionListMaxMembersPropertySize
			{
				get
				{
					return 15000;
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000809B0 File Offset: 0x0007EBB0
			public int DistributionListMaxNumberOfEntries
			{
				get
				{
					return 10000;
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x000809B7 File Offset: 0x0007EBB7
			public int DefaultFolderMinimumSuffix
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000809BA File Offset: 0x0007EBBA
			public int DefaultFolderMaximumSuffix
			{
				get
				{
					return 9;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x000809BE File Offset: 0x0007EBBE
			public int DefaultFolderDataCacheMaxRowCount
			{
				get
				{
					return 1024;
				}
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x000809C5 File Offset: 0x0007EBC5
			public int NotificationsMaxSubscriptions
			{
				get
				{
					return 1024;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x000809CC File Offset: 0x0007EBCC
			public int MaxDelegates
			{
				get
				{
					return 100000;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000809D3 File Offset: 0x0007EBD3
			public BufferPoolCollection.BufferSize PropertyStreamPageSize
			{
				get
				{
					return BufferPoolCollection.BufferSize.Size256K;
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000809D7 File Offset: 0x0007EBD7
			public long ConversionsFolderMaxTotalMessageSize
			{
				get
				{
					return 134217728L;
				}
			}
		}
	}
}
