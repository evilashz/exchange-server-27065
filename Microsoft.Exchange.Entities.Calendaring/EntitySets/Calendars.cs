using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class Calendars : StorageEntitySet<Calendars, Calendar, IMailboxSession>, ICalendars, IEntitySet<Calendar>
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000059E0 File Offset: 0x00003BE0
		protected Calendars(IStorageEntitySetScope<IMailboxSession> parentScope, CalendarGroupReference calendarGroupForNewCalendars, IEntityCommandFactory<Calendars, Calendar> commandFactory = null) : base(parentScope, "Calendars", commandFactory ?? EntityCommandFactory<Calendars, Calendar, CreateCalendar, DeleteCalendar, FindCalendars, ReadCalendar, UpdateCalendar>.Instance)
		{
			this.CalendarGroupForNewCalendars = calendarGroupForNewCalendars;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005A00 File Offset: 0x00003C00
		public virtual CalendarFolderDataProvider CalendarFolderDataProvider
		{
			get
			{
				CalendarFolderDataProvider result;
				if ((result = this.calendarFolderDataProvider) == null)
				{
					result = (this.calendarFolderDataProvider = new CalendarFolderDataProvider(this, base.Session.GetDefaultFolderId(DefaultFolderType.Calendar)));
				}
				return result;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005A34 File Offset: 0x00003C34
		public virtual CalendarGroupEntryDataProvider CalendarGroupEntryDataProvider
		{
			get
			{
				CalendarGroupEntryDataProvider result;
				if ((result = this.calendarGroupEntryDataProvider) == null)
				{
					result = (this.calendarGroupEntryDataProvider = new CalendarGroupEntryDataProvider(this));
				}
				return result;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005A5A File Offset: 0x00003C5A
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005A62 File Offset: 0x00003C62
		public CalendarGroupReference CalendarGroupForNewCalendars { get; private set; }

		// Token: 0x17000053 RID: 83
		public ICalendarReference this[string calendarId]
		{
			get
			{
				return new CalendarReference(this, calendarId);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005A74 File Offset: 0x00003C74
		public Calendar Create(string name)
		{
			return base.Create(new Calendar
			{
				Name = name
			}, null);
		}

		// Token: 0x04000058 RID: 88
		private CalendarFolderDataProvider calendarFolderDataProvider;

		// Token: 0x04000059 RID: 89
		private CalendarGroupEntryDataProvider calendarGroupEntryDataProvider;
	}
}
