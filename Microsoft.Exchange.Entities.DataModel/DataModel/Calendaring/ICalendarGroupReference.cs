using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000054 RID: 84
	public interface ICalendarGroupReference : IEntityReference<CalendarGroup>
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002DB RID: 731
		ICalendars Calendars { get; }
	}
}
