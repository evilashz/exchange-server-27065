using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C5D RID: 3165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FavoriteFolderEntrySchema : FolderTreeDataSchema
	{
		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06006F7A RID: 28538 RVA: 0x001E00A8 File Offset: 0x001DE2A8
		public new static FavoriteFolderEntrySchema Instance
		{
			get
			{
				if (FavoriteFolderEntrySchema.instance == null)
				{
					FavoriteFolderEntrySchema.instance = new FavoriteFolderEntrySchema();
				}
				return FavoriteFolderEntrySchema.instance;
			}
		}

		// Token: 0x0400439B RID: 17307
		[Autoload]
		public static readonly StorePropertyDefinition StoreEntryId = InternalSchema.NavigationNodeStoreEntryId;

		// Token: 0x0400439C RID: 17308
		[Autoload]
		public static readonly StorePropertyDefinition NodeEntryId = InternalSchema.NavigationNodeEntryId;

		// Token: 0x0400439D RID: 17309
		[Autoload]
		public static readonly StorePropertyDefinition NavigationNodeRecordKey = InternalSchema.NavigationNodeRecordKey;

		// Token: 0x0400439E RID: 17310
		[Autoload]
		public static readonly StorePropertyDefinition FolderName = InternalSchema.Subject;

		// Token: 0x0400439F RID: 17311
		private static FavoriteFolderEntrySchema instance;
	}
}
