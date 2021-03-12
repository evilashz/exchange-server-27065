using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CDE RID: 3294
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroupEntrySchema : FolderTreeDataSchema
	{
		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06007201 RID: 29185 RVA: 0x001F8D5D File Offset: 0x001F6F5D
		public new static TaskGroupEntrySchema Instance
		{
			get
			{
				if (TaskGroupEntrySchema.instance == null)
				{
					TaskGroupEntrySchema.instance = new TaskGroupEntrySchema();
				}
				return TaskGroupEntrySchema.instance;
			}
		}

		// Token: 0x04004F18 RID: 20248
		[Autoload]
		public static readonly StorePropertyDefinition StoreEntryId = InternalSchema.NavigationNodeStoreEntryId;

		// Token: 0x04004F19 RID: 20249
		[Autoload]
		public static readonly StorePropertyDefinition NodeRecordKey = InternalSchema.NavigationNodeRecordKey;

		// Token: 0x04004F1A RID: 20250
		[Autoload]
		public static readonly StorePropertyDefinition NodeEntryId = InternalSchema.NavigationNodeEntryId;

		// Token: 0x04004F1B RID: 20251
		[Autoload]
		public static readonly StorePropertyDefinition ParentGroupName = InternalSchema.NavigationNodeGroupName;

		// Token: 0x04004F1C RID: 20252
		private static TaskGroupEntrySchema instance;
	}
}
