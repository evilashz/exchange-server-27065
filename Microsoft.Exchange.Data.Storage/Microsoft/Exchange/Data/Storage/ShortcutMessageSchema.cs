using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC9 RID: 3273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ShortcutMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x06007194 RID: 29076 RVA: 0x001F7A2F File Offset: 0x001F5C2F
		public new static ShortcutMessageSchema Instance
		{
			get
			{
				if (ShortcutMessageSchema.instance == null)
				{
					ShortcutMessageSchema.instance = new ShortcutMessageSchema();
				}
				return ShortcutMessageSchema.instance;
			}
		}

		// Token: 0x04004EF4 RID: 20212
		[Autoload]
		public static readonly StorePropertyDefinition FavPublicSourceKey = InternalSchema.FavPublicSourceKey;

		// Token: 0x04004EF5 RID: 20213
		[Autoload]
		public static readonly StorePropertyDefinition FavoriteDisplayAlias = InternalSchema.FavoriteDisplayAlias;

		// Token: 0x04004EF6 RID: 20214
		[Autoload]
		public static readonly StorePropertyDefinition FavoriteDisplayName = InternalSchema.FavoriteDisplayName;

		// Token: 0x04004EF7 RID: 20215
		private static ShortcutMessageSchema instance;
	}
}
