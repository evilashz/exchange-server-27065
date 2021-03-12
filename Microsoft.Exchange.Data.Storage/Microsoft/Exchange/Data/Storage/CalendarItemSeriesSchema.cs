using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C28 RID: 3112
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItemSeriesSchema : CalendarItemBaseSchema
	{
		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x06006E99 RID: 28313 RVA: 0x001DBEAE File Offset: 0x001DA0AE
		public new static CalendarItemSeriesSchema Instance
		{
			get
			{
				if (CalendarItemSeriesSchema.instance == null)
				{
					CalendarItemSeriesSchema.instance = new CalendarItemSeriesSchema();
				}
				return CalendarItemSeriesSchema.instance;
			}
		}

		// Token: 0x04004202 RID: 16898
		[Autoload]
		public static readonly StorePropertyDefinition CalendarInteropActionQueue = InternalSchema.CalendarInteropActionQueue;

		// Token: 0x04004203 RID: 16899
		[Autoload]
		public static readonly StorePropertyDefinition CalendarInteropActionQueueHasData = InternalSchema.CalendarInteropActionQueueHasData;

		// Token: 0x04004204 RID: 16900
		[Autoload]
		public static readonly StorePropertyDefinition SeriesCreationHash = InternalSchema.SeriesCreationHash;

		// Token: 0x04004205 RID: 16901
		[Autoload]
		public static readonly StorePropertyDefinition SeriesReminderIsSet = InternalSchema.SeriesReminderIsSet;

		// Token: 0x04004206 RID: 16902
		public static readonly StorePropertyDefinition CalendarInteropActionQueueHasDataInternal = InternalSchema.CalendarInteropActionQueueHasDataInternal;

		// Token: 0x04004207 RID: 16903
		public static readonly StorePropertyDefinition CalendarInteropActionQueueInternal = InternalSchema.CalendarInteropActionQueueInternal;

		// Token: 0x04004208 RID: 16904
		private static CalendarItemSeriesSchema instance = null;
	}
}
