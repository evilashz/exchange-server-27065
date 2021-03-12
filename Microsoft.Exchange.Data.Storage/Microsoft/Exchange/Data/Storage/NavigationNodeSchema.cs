using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C90 RID: 3216
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NavigationNodeSchema : StoreObjectSchema
	{
		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x0600707A RID: 28794 RVA: 0x001F2546 File Offset: 0x001F0746
		public new static NavigationNodeSchema Instance
		{
			get
			{
				if (NavigationNodeSchema.instance == null)
				{
					NavigationNodeSchema.instance = new NavigationNodeSchema();
				}
				return NavigationNodeSchema.instance;
			}
		}

		// Token: 0x04004D9F RID: 19871
		public static readonly StorePropertyDefinition GroupClassId = InternalSchema.NavigationNodeGroupClassId;

		// Token: 0x04004DA0 RID: 19872
		public static readonly StorePropertyDefinition StoreEntryId = InternalSchema.NavigationNodeStoreEntryId;

		// Token: 0x04004DA1 RID: 19873
		public static readonly StorePropertyDefinition NodeRecordKey = InternalSchema.NavigationNodeRecordKey;

		// Token: 0x04004DA2 RID: 19874
		public static readonly StorePropertyDefinition ParentGroupClassId = InternalSchema.NavigationNodeParentGroupClassId;

		// Token: 0x04004DA3 RID: 19875
		public static readonly StorePropertyDefinition GroupName = InternalSchema.NavigationNodeGroupName;

		// Token: 0x04004DA4 RID: 19876
		public static readonly StorePropertyDefinition CalendarColor = InternalSchema.NavigationNodeCalendarColor;

		// Token: 0x04004DA5 RID: 19877
		public static readonly StorePropertyDefinition NodeEntryId = InternalSchema.NavigationNodeEntryId;

		// Token: 0x04004DA6 RID: 19878
		public static readonly StorePropertyDefinition Type = InternalSchema.NavigationNodeType;

		// Token: 0x04004DA7 RID: 19879
		public static readonly StorePropertyDefinition OutlookTagId = InternalSchema.NavigationNodeOutlookTagId;

		// Token: 0x04004DA8 RID: 19880
		public static readonly StorePropertyDefinition Flags = InternalSchema.NavigationNodeFlags;

		// Token: 0x04004DA9 RID: 19881
		public static readonly StorePropertyDefinition Ordinal = InternalSchema.NavigationNodeOrdinal;

		// Token: 0x04004DAA RID: 19882
		public static readonly StorePropertyDefinition ClassId = InternalSchema.NavigationNodeClassId;

		// Token: 0x04004DAB RID: 19883
		public static readonly StorePropertyDefinition GroupSection = InternalSchema.NavigationNodeGroupSection;

		// Token: 0x04004DAC RID: 19884
		public static readonly StorePropertyDefinition AddressBookEntryId = InternalSchema.NavigationNodeAddressBookEntryId;

		// Token: 0x04004DAD RID: 19885
		public static readonly StorePropertyDefinition AddressBookStoreEntryId = InternalSchema.NavigationNodeAddressBookStoreEntryId;

		// Token: 0x04004DAE RID: 19886
		public static readonly StorePropertyDefinition CalendarTypeFromOlderExchange = InternalSchema.NavigationNodeCalendarTypeFromOlderExchange;

		// Token: 0x04004DAF RID: 19887
		private static NavigationNodeSchema instance = null;
	}
}
