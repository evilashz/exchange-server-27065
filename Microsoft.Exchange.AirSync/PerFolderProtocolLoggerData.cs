using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000101 RID: 257
	internal enum PerFolderProtocolLoggerData
	{
		// Token: 0x04000907 RID: 2311
		FolderId,
		// Token: 0x04000908 RID: 2312
		FolderDataType,
		// Token: 0x04000909 RID: 2313
		SyncType,
		// Token: 0x0400090A RID: 2314
		FilterType,
		// Token: 0x0400090B RID: 2315
		SmsFilterType,
		// Token: 0x0400090C RID: 2316
		ClientSyncKey,
		// Token: 0x0400090D RID: 2317
		ServerSyncKey,
		// Token: 0x0400090E RID: 2318
		SyncStateKb,
		// Token: 0x0400090F RID: 2319
		SyncStateKbLeftCompressed,
		// Token: 0x04000910 RID: 2320
		ClientAdds,
		// Token: 0x04000911 RID: 2321
		ClientChanges,
		// Token: 0x04000912 RID: 2322
		ClientDeletes,
		// Token: 0x04000913 RID: 2323
		ClientFetches,
		// Token: 0x04000914 RID: 2324
		ClientFailedToConvert,
		// Token: 0x04000915 RID: 2325
		ClientFailedToSend,
		// Token: 0x04000916 RID: 2326
		ClientSends,
		// Token: 0x04000917 RID: 2327
		ServerAdds,
		// Token: 0x04000918 RID: 2328
		ServerChanges,
		// Token: 0x04000919 RID: 2329
		ServerDeletes,
		// Token: 0x0400091A RID: 2330
		ServerSoftDeletes,
		// Token: 0x0400091B RID: 2331
		ServerFailedToConvert,
		// Token: 0x0400091C RID: 2332
		ServerChangeTrackingRejected,
		// Token: 0x0400091D RID: 2333
		PerFolderStatus,
		// Token: 0x0400091E RID: 2334
		ServerAssociatedAdds,
		// Token: 0x0400091F RID: 2335
		SkippedDeletes,
		// Token: 0x04000920 RID: 2336
		BodyRequested,
		// Token: 0x04000921 RID: 2337
		BodyPartRequested,
		// Token: 0x04000922 RID: 2338
		SyncStateKbCommitted,
		// Token: 0x04000923 RID: 2339
		TotalSaveCount,
		// Token: 0x04000924 RID: 2340
		ColdSaveCount,
		// Token: 0x04000925 RID: 2341
		ColdCopyCount,
		// Token: 0x04000926 RID: 2342
		TotalLoadCount,
		// Token: 0x04000927 RID: 2343
		MidnightRollover,
		// Token: 0x04000928 RID: 2344
		FirstTimeSyncItemsDiscarded,
		// Token: 0x04000929 RID: 2345
		ProviderSyncType,
		// Token: 0x0400092A RID: 2346
		GetChangesIterations,
		// Token: 0x0400092B RID: 2347
		GetChangesTime
	}
}
