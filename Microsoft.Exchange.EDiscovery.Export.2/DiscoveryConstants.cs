using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000008 RID: 8
	internal static class DiscoveryConstants
	{
		// Token: 0x04000021 RID: 33
		public const string EwsTypeNamespace = "http://schemas.microsoft.com/exchange/services/2006/types";

		// Token: 0x04000022 RID: 34
		public const string OpenAsAdminOrSystemService = "OpenAsAdminOrSystemService";

		// Token: 0x04000023 RID: 35
		public const string ConnectingSID = "ConnectingSID";

		// Token: 0x04000024 RID: 36
		public const string SearchScopeType = "SearchScopeType";

		// Token: 0x04000025 RID: 37
		public const string AutoDetectScopeType = "AutoDetect";

		// Token: 0x04000026 RID: 38
		public const string SavedSearchScopeType = "SavedSearchId";

		// Token: 0x04000027 RID: 39
		public const string SearchType = "SearchType";

		// Token: 0x04000028 RID: 40
		public const string ExpandSourcesSearchType = "ExpandSources";

		// Token: 0x04000029 RID: 41
		public const string NonIndexableItemDetailsSearchType = "NonIndexedItemPreview";

		// Token: 0x0400002A RID: 42
		public const string NonIndexableItemStatisticsSearchType = "NonIndexedItemStatistics";

		// Token: 0x0400002B RID: 43
		public const string PublicFolderMarker = "\\";

		// Token: 0x0400002C RID: 44
		public const long DefaultPSTSizeLimitInBytes = 10000000000L;

		// Token: 0x0400002D RID: 45
		public const int DefaultSearchMailboxesPageSize = 500;

		// Token: 0x0400002E RID: 46
		public const int DefaultExportBatchItemCountLimit = 250;

		// Token: 0x0400002F RID: 47
		public const int DefaultExportBatchSizeLimit = 5242880;

		// Token: 0x04000030 RID: 48
		public const int DefaultItemIdListCacheSize = 500;

		// Token: 0x04000031 RID: 49
		public const int DefaultRetryInterval = 30000;

		// Token: 0x04000032 RID: 50
		public const int DefaultMaxCSVLogFileSizeInBytes = 104857600;

		// Token: 0x04000033 RID: 51
		public const bool DefaultPartitionCSVLogFile = true;

		// Token: 0x04000034 RID: 52
		public const int DefaultAutoDiscoverBatchSize = 50;

		// Token: 0x04000035 RID: 53
		public static readonly TimeSpan DefaultTotalRetryTimeWindow = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000036 RID: 54
		public static readonly TimeSpan[] DefaultRetrySchedule = new TimeSpan[]
		{
			TimeSpan.FromSeconds(30.0),
			TimeSpan.FromMinutes(2.0),
			TimeSpan.FromMinutes(6.0)
		};
	}
}
