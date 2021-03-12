using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C4A RID: 3146
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationActionItemSchema : ItemSchema
	{
		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06006F3E RID: 28478 RVA: 0x001DEE1B File Offset: 0x001DD01B
		public new static ConversationActionItemSchema Instance
		{
			get
			{
				if (ConversationActionItemSchema.instance == null)
				{
					ConversationActionItemSchema.instance = new ConversationActionItemSchema();
				}
				return ConversationActionItemSchema.instance;
			}
		}

		// Token: 0x04004300 RID: 17152
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionMoveFolderId = InternalSchema.ConversationActionMoveFolderId;

		// Token: 0x04004301 RID: 17153
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionMoveStoreId = InternalSchema.ConversationActionMoveStoreId;

		// Token: 0x04004302 RID: 17154
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionPolicyTag = InternalSchema.ConversationActionPolicyTag;

		// Token: 0x04004303 RID: 17155
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionMaxDeliveryTime = InternalSchema.ConversationActionMaxDeliveryTime;

		// Token: 0x04004304 RID: 17156
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionLastMoveFolderId = InternalSchema.ConversationActionLastMoveFolderId;

		// Token: 0x04004305 RID: 17157
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionLastCategorySet = InternalSchema.ConversationActionLastCategorySet;

		// Token: 0x04004306 RID: 17158
		[Autoload]
		public static readonly StorePropertyDefinition ConversationActionVersion = InternalSchema.ConversationActionVersion;

		// Token: 0x04004307 RID: 17159
		private static ConversationActionItemSchema instance = null;
	}
}
