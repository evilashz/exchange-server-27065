using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C22 RID: 3106
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderTreeDataSchema : MessageItemSchema
	{
		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06006E7A RID: 28282 RVA: 0x001DB298 File Offset: 0x001D9498
		public new static FolderTreeDataSchema Instance
		{
			get
			{
				if (FolderTreeDataSchema.instance == null)
				{
					FolderTreeDataSchema.instance = new FolderTreeDataSchema();
				}
				return FolderTreeDataSchema.instance;
			}
		}

		// Token: 0x04004137 RID: 16695
		[Autoload]
		public static readonly StorePropertyDefinition Type = InternalSchema.NavigationNodeType;

		// Token: 0x04004138 RID: 16696
		[Autoload]
		public static readonly StorePropertyDefinition OutlookTagId = InternalSchema.NavigationNodeOutlookTagId;

		// Token: 0x04004139 RID: 16697
		[Autoload]
		public static readonly StorePropertyDefinition Ordinal = InternalSchema.NavigationNodeOrdinal;

		// Token: 0x0400413A RID: 16698
		[Autoload]
		public static readonly StorePropertyDefinition ClassId = InternalSchema.NavigationNodeClassId;

		// Token: 0x0400413B RID: 16699
		[Autoload]
		public static readonly StorePropertyDefinition GroupSection = InternalSchema.NavigationNodeGroupSection;

		// Token: 0x0400413C RID: 16700
		[Autoload]
		public static readonly StorePropertyDefinition ParentGroupClassId = InternalSchema.NavigationNodeParentGroupClassId;

		// Token: 0x0400413D RID: 16701
		[Autoload]
		public static readonly StorePropertyDefinition FolderTreeDataFlags = InternalSchema.NavigationNodeFlags;

		// Token: 0x0400413E RID: 16702
		private static FolderTreeDataSchema instance;
	}
}
