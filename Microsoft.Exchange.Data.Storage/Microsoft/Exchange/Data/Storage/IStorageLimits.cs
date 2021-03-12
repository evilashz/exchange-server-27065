using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000288 RID: 648
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStorageLimits
	{
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001AD9 RID: 6873
		int NamedPropertyNameMaximumLength { get; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001ADA RID: 6874
		int UserConfigurationMaxSearched { get; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001ADB RID: 6875
		int FindNamesViewResultsLimit { get; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001ADC RID: 6876
		int AmbiguousNamesViewResultsLimit { get; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001ADD RID: 6877
		int CalendarSingleInstanceLimit { get; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001ADE RID: 6878
		int CalendarExpansionInstanceLimit { get; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001ADF RID: 6879
		int CalendarExpansionMaxMasters { get; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001AE0 RID: 6880
		int CalendarMaxNumberVEventsForICalImport { get; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001AE1 RID: 6881
		int CalendarMaxNumberBytesForICalImport { get; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001AE2 RID: 6882
		int RecurrenceMaximumInterval { get; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001AE3 RID: 6883
		int RecurrenceMaximumNumberedOccurrences { get; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001AE4 RID: 6884
		int DistributionListMaxMembersPropertySize { get; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001AE5 RID: 6885
		int DistributionListMaxNumberOfEntries { get; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001AE6 RID: 6886
		int DefaultFolderMaximumSuffix { get; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001AE7 RID: 6887
		int DefaultFolderMinimumSuffix { get; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001AE8 RID: 6888
		int DefaultFolderDataCacheMaxRowCount { get; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001AE9 RID: 6889
		int NotificationsMaxSubscriptions { get; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001AEA RID: 6890
		int MaxDelegates { get; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001AEB RID: 6891
		BufferPoolCollection.BufferSize PropertyStreamPageSize { get; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001AEC RID: 6892
		long ConversionsFolderMaxTotalMessageSize { get; }
	}
}
