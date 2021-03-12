using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarsInCalendarGroup : Calendars
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00005A96 File Offset: 0x00003C96
		public CalendarsInCalendarGroup(CalendarGroupReference calendarGroup) : base(calendarGroup, calendarGroup, null)
		{
			this.CalendarGroup = calendarGroup;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005AA8 File Offset: 0x00003CA8
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public CalendarGroupReference CalendarGroup { get; private set; }
	}
}
