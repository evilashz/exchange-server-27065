using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000059 RID: 89
	public interface ICalendars : IEntitySet<Calendar>
	{
		// Token: 0x1700012F RID: 303
		ICalendarReference this[string calendarId]
		{
			get;
		}

		// Token: 0x060002EC RID: 748
		Calendar Create(string name);
	}
}
