using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000057 RID: 87
	public interface ICalendaringContainer
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002E7 RID: 743
		IMailboxCalendars Calendars { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002E8 RID: 744
		ICalendarGroups CalendarGroups { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002E9 RID: 745
		IMeetingRequestMessages MeetingRequestMessages { get; }
	}
}
