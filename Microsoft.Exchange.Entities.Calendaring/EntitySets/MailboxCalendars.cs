using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxCalendars : Calendars, IMailboxCalendars, ICalendars, IEntitySet<Calendar>
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000987B File Offset: 0x00007A7B
		public MailboxCalendars(IStorageEntitySetScope<IMailboxSession> scope, CalendarGroupReference calendarGroupForNewCalendars) : base(scope, calendarGroupForNewCalendars, null)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009888 File Offset: 0x00007A88
		public CalendarReference Default
		{
			get
			{
				CalendarReference result;
				if ((result = this.defaultCalendar) == null)
				{
					result = (this.defaultCalendar = new DefaultCalendarReference(this));
				}
				return result;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000098AE File Offset: 0x00007AAE
		ICalendarReference IMailboxCalendars.Default
		{
			get
			{
				return this.Default;
			}
		}

		// Token: 0x040000A9 RID: 169
		private CalendarReference defaultCalendar;
	}
}
