using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C66 RID: 3174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class HierarchySyncMetadataItemSchema : ItemSchema
	{
		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x06006FBC RID: 28604 RVA: 0x001E13A7 File Offset: 0x001DF5A7
		public new static HierarchySyncMetadataItemSchema Instance
		{
			get
			{
				if (HierarchySyncMetadataItemSchema.instance == null)
				{
					HierarchySyncMetadataItemSchema.instance = new HierarchySyncMetadataItemSchema();
				}
				return HierarchySyncMetadataItemSchema.instance;
			}
		}

		// Token: 0x040043C2 RID: 17346
		private static HierarchySyncMetadataItemSchema instance = null;

		// Token: 0x040043C3 RID: 17347
		[Autoload]
		public static readonly StorePropertyDefinition LastAttemptedSyncTime = InternalSchema.HierarchySyncLastAttemptedSyncTime;

		// Token: 0x040043C4 RID: 17348
		[Autoload]
		public static readonly StorePropertyDefinition LastFailedSyncTime = InternalSchema.HierarchySyncLastFailedSyncTime;

		// Token: 0x040043C5 RID: 17349
		[Autoload]
		public static readonly StorePropertyDefinition LastSuccessfulSyncTime = InternalSchema.HierarchySyncLastSuccessfulSyncTime;

		// Token: 0x040043C6 RID: 17350
		[Autoload]
		public static readonly StorePropertyDefinition FirstFailedSyncTimeAfterLastSuccess = InternalSchema.HierarchySyncFirstFailedSyncTimeAfterLastSuccess;

		// Token: 0x040043C7 RID: 17351
		[Autoload]
		public static readonly StorePropertyDefinition LastSyncFailure = InternalSchema.HierarchySyncLastSyncFailure;

		// Token: 0x040043C8 RID: 17352
		[Autoload]
		public static readonly StorePropertyDefinition NumberOfAttemptsAfterLastSuccess = InternalSchema.HierarchySyncNumberOfAttemptsAfterLastSuccess;

		// Token: 0x040043C9 RID: 17353
		[Autoload]
		public static readonly StorePropertyDefinition NumberOfBatchesExecuted = InternalSchema.HierarchySyncNumberOfBatchesExecuted;

		// Token: 0x040043CA RID: 17354
		[Autoload]
		public static readonly StorePropertyDefinition NumberOfFoldersSynced = InternalSchema.HierarchySyncNumberOfFoldersSynced;

		// Token: 0x040043CB RID: 17355
		[Autoload]
		public static readonly StorePropertyDefinition NumberOfFoldersToBeSynced = InternalSchema.HierarchySyncNumberOfFoldersToBeSynced;

		// Token: 0x040043CC RID: 17356
		[Autoload]
		public static readonly StorePropertyDefinition BatchSize = InternalSchema.HierarchySyncBatchSize;
	}
}
