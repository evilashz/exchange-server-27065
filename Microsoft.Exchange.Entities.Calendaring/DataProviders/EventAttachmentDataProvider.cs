using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.EntitySets;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x0200001E RID: 30
	internal class EventAttachmentDataProvider : AttachmentDataProvider
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000036DC File Offset: 0x000018DC
		public EventAttachmentDataProvider(EventReference scope, StoreId parentItemId) : base(scope, parentItemId)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000036E8 File Offset: 0x000018E8
		protected override void BeforeParentItemSave(IItem parentItem)
		{
			ICalendarItemBase calendarItemBase = parentItem as ICalendarItemBase;
			if (calendarItemBase != null)
			{
				bool flag;
				CalendarItemAccessors.HasAttendees.TryGetValue(calendarItemBase, out flag);
				if (flag)
				{
					calendarItemBase[CalendarItemBaseSchema.IsDraft] = true;
					return;
				}
				calendarItemBase[CalendarItemBaseSchema.IsDraft] = false;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003734 File Offset: 0x00001934
		protected override AttachmentCollection GetAttachmentCollection(IItem parentItem)
		{
			ICalendarItemOccurrence calendarItemOccurrence = parentItem as ICalendarItemOccurrence;
			if (calendarItemOccurrence != null && !calendarItemOccurrence.IsException)
			{
				calendarItemOccurrence.MakeModifiedOccurrence();
			}
			return base.GetAttachmentCollection(parentItem);
		}
	}
}
