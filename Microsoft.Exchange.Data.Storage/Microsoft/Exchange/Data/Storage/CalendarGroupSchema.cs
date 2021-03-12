using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C23 RID: 3107
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarGroupSchema : FolderTreeDataSchema
	{
		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06006E7D RID: 28285 RVA: 0x001DB30B File Offset: 0x001D950B
		public new static CalendarGroupSchema Instance
		{
			get
			{
				if (CalendarGroupSchema.instance == null)
				{
					CalendarGroupSchema.instance = new CalendarGroupSchema();
				}
				return CalendarGroupSchema.instance;
			}
		}

		// Token: 0x0400413F RID: 16703
		[Autoload]
		public static readonly StorePropertyDefinition GroupClassId = InternalSchema.NavigationNodeGroupClassId;

		// Token: 0x04004140 RID: 16704
		private static CalendarGroupSchema instance;
	}
}
