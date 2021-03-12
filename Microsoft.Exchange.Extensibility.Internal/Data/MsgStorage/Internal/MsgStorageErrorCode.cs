using System;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public enum MsgStorageErrorCode
	{
		// Token: 0x04000545 RID: 1349
		Undetermined,
		// Token: 0x04000546 RID: 1350
		CreateFileFailed,
		// Token: 0x04000547 RID: 1351
		CreateStorageOnStreamFailed,
		// Token: 0x04000548 RID: 1352
		OpenStorageFileFailed,
		// Token: 0x04000549 RID: 1353
		OpenStorageOnStreamFailed,
		// Token: 0x0400054A RID: 1354
		FailedRead,
		// Token: 0x0400054B RID: 1355
		FailedWrite,
		// Token: 0x0400054C RID: 1356
		FailedSeek,
		// Token: 0x0400054D RID: 1357
		FailedCreateStream,
		// Token: 0x0400054E RID: 1358
		FailedOpenStream,
		// Token: 0x0400054F RID: 1359
		FailedCreateSubstorage,
		// Token: 0x04000550 RID: 1360
		FailedOpenSubstorage,
		// Token: 0x04000551 RID: 1361
		FailedCopyStorage,
		// Token: 0x04000552 RID: 1362
		FailedWriteOle,
		// Token: 0x04000553 RID: 1363
		StorageStreamTooLong,
		// Token: 0x04000554 RID: 1364
		StorageStreamTruncated,
		// Token: 0x04000555 RID: 1365
		InvalidPropertyType,
		// Token: 0x04000556 RID: 1366
		PropertyListTruncated,
		// Token: 0x04000557 RID: 1367
		PropertyValueTruncated,
		// Token: 0x04000558 RID: 1368
		InvalidPropertyValueLength,
		// Token: 0x04000559 RID: 1369
		MultivaluedPropertyDimensionTooLarge,
		// Token: 0x0400055A RID: 1370
		MultivaluedValueTooLong,
		// Token: 0x0400055B RID: 1371
		RecipientPropertyTooLong,
		// Token: 0x0400055C RID: 1372
		NonStreamablePropertyTooLong,
		// Token: 0x0400055D RID: 1373
		NamedPropertyNotFound,
		// Token: 0x0400055E RID: 1374
		CorruptNamedPropertyData,
		// Token: 0x0400055F RID: 1375
		NamedPropertiesListTooLong,
		// Token: 0x04000560 RID: 1376
		EmptyPropertiesStream
	}
}
