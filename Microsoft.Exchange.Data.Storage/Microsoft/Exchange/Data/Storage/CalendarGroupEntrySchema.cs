using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C29 RID: 3113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarGroupEntrySchema : FolderTreeDataSchema
	{
		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06006E9C RID: 28316 RVA: 0x001DBF1F File Offset: 0x001DA11F
		public new static CalendarGroupEntrySchema Instance
		{
			get
			{
				if (CalendarGroupEntrySchema.instance == null)
				{
					CalendarGroupEntrySchema.instance = new CalendarGroupEntrySchema();
				}
				return CalendarGroupEntrySchema.instance;
			}
		}

		// Token: 0x04004209 RID: 16905
		[Autoload]
		public static readonly StorePropertyDefinition StoreEntryId = InternalSchema.NavigationNodeStoreEntryId;

		// Token: 0x0400420A RID: 16906
		[Autoload]
		public static readonly StorePropertyDefinition NodeRecordKey = InternalSchema.NavigationNodeRecordKey;

		// Token: 0x0400420B RID: 16907
		[Autoload]
		public static readonly StorePropertyDefinition CalendarColor = InternalSchema.NavigationNodeCalendarColor;

		// Token: 0x0400420C RID: 16908
		[Autoload]
		public static readonly StorePropertyDefinition CalendarName = InternalSchema.Subject;

		// Token: 0x0400420D RID: 16909
		[Autoload]
		public static readonly StorePropertyDefinition NodeEntryId = InternalSchema.NavigationNodeEntryId;

		// Token: 0x0400420E RID: 16910
		[Autoload]
		public static readonly StorePropertyDefinition ParentGroupName = InternalSchema.NavigationNodeGroupName;

		// Token: 0x0400420F RID: 16911
		[Autoload]
		public static readonly StorePropertyDefinition SharerAddressBookEntryId = InternalSchema.NavigationNodeAddressBookEntryId;

		// Token: 0x04004210 RID: 16912
		[Autoload]
		public static readonly StorePropertyDefinition UserAddressBookStoreEntryId = InternalSchema.NavigationNodeAddressBookStoreEntryId;

		// Token: 0x04004211 RID: 16913
		private static CalendarGroupEntrySchema instance;
	}
}
