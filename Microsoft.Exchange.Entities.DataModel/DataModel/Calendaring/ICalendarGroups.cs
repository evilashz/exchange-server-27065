using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000056 RID: 86
	public interface ICalendarGroups : IEntitySet<CalendarGroup>
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002E3 RID: 739
		ICalendarGroupReference MyCalendars { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002E4 RID: 740
		ICalendarGroupReference OtherCalendars { get; }

		// Token: 0x1700012A RID: 298
		ICalendarGroupReference this[string calendarGroupId]
		{
			get;
		}

		// Token: 0x060002E6 RID: 742
		CalendarGroup Create(string name);
	}
}
