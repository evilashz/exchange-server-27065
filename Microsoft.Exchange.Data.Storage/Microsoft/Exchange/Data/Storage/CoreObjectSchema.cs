using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C51 RID: 3153
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CoreObjectSchema : Schema
	{
		// Token: 0x06006F5B RID: 28507 RVA: 0x001DF7C8 File Offset: 0x001DD9C8
		protected CoreObjectSchema()
		{
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x06006F5C RID: 28508 RVA: 0x001DF7D0 File Offset: 0x001DD9D0
		public new static CoreObjectSchema Instance
		{
			get
			{
				if (CoreObjectSchema.instance == null)
				{
					CoreObjectSchema.instance = new CoreObjectSchema();
				}
				return CoreObjectSchema.instance;
			}
		}

		// Token: 0x0400433A RID: 17210
		private static CoreObjectSchema instance = null;

		// Token: 0x0400433B RID: 17211
		public static readonly StorePropertyDefinition ChangeKey = InternalSchema.ChangeKey;

		// Token: 0x0400433C RID: 17212
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition CreationTime = InternalSchema.CreationTime;

		// Token: 0x0400433D RID: 17213
		public static readonly StorePropertyDefinition DeletedOnTime = InternalSchema.DeletedOnTime;

		// Token: 0x0400433E RID: 17214
		[Autoload]
		public static readonly StorePropertyDefinition EntryId = InternalSchema.EntryId;

		// Token: 0x0400433F RID: 17215
		public static readonly StorePropertyDefinition LastModifiedTime = InternalSchema.LastModifiedTime;

		// Token: 0x04004340 RID: 17216
		[Autoload]
		public static readonly StorePropertyDefinition ParentEntryId = InternalSchema.ParentEntryId;

		// Token: 0x04004341 RID: 17217
		[Autoload]
		public static readonly StorePropertyDefinition ContentClass = InternalSchema.ContentClass;

		// Token: 0x04004342 RID: 17218
		[Autoload]
		public static readonly StorePropertyDefinition ParentFid = InternalSchema.ParentFid;

		// Token: 0x04004343 RID: 17219
		internal static readonly StorePropertyDefinition ParentSourceKey = InternalSchema.ParentSourceKey;

		// Token: 0x04004344 RID: 17220
		internal static readonly StorePropertyDefinition PredecessorChangeList = InternalSchema.PredecessorChangeList;

		// Token: 0x04004345 RID: 17221
		internal static readonly StorePropertyDefinition SourceKey = InternalSchema.SourceKey;

		// Token: 0x04004346 RID: 17222
		public static readonly StorePropertyDefinition[] AllPropertiesOnStore = InternalSchema.ContentConversionProperties;
	}
}
