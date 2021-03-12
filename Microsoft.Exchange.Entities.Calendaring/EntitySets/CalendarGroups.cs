using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarGroupCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarGroups : StorageEntitySet<CalendarGroups, CalendarGroup, IMailboxSession>, ICalendarGroups, IEntitySet<CalendarGroup>
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005877 File Offset: 0x00003A77
		protected internal CalendarGroups(IStorageEntitySetScope<IMailboxSession> parentScope, IEntityCommandFactory<CalendarGroups, CalendarGroup> commandFactory = null) : base(parentScope, "CalendarGroups", commandFactory ?? EntityCommandFactory<CalendarGroups, CalendarGroup, CreateCalendarGroup, DeleteCalendarGroup, FindCalendarGroups, ReadCalendarGroup, UpdateCalendarGroup>.Instance)
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005890 File Offset: 0x00003A90
		public CalendarGroupReference MyCalendars
		{
			get
			{
				CalendarGroupReference result;
				if ((result = this.myCalendars) == null)
				{
					result = (this.myCalendars = new CalendarGroupReference(this, CalendarGroupType.MyCalendars));
				}
				return result;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000058B8 File Offset: 0x00003AB8
		public CalendarGroupReference OtherCalendars
		{
			get
			{
				CalendarGroupReference result;
				if ((result = this.otherCalendars) == null)
				{
					result = (this.otherCalendars = new CalendarGroupReference(this, CalendarGroupType.OtherCalendars));
				}
				return result;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000058DF File Offset: 0x00003ADF
		ICalendarGroupReference ICalendarGroups.MyCalendars
		{
			get
			{
				return this.MyCalendars;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000058E7 File Offset: 0x00003AE7
		ICalendarGroupReference ICalendarGroups.OtherCalendars
		{
			get
			{
				return this.OtherCalendars;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000058F0 File Offset: 0x00003AF0
		internal virtual CalendarGroupDataProvider CalendarGroupDataProvider
		{
			get
			{
				CalendarGroupDataProvider result;
				if ((result = this.calendarGroupDataProvider) == null)
				{
					result = (this.calendarGroupDataProvider = new CalendarGroupDataProvider(this));
				}
				return result;
			}
		}

		// Token: 0x1700004D RID: 77
		public CalendarGroupReference this[string calendarGroupId]
		{
			get
			{
				return new CalendarGroupReference(this, calendarGroupId);
			}
		}

		// Token: 0x1700004E RID: 78
		ICalendarGroupReference ICalendarGroups.this[string calendarGroupId]
		{
			get
			{
				return this[calendarGroupId];
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005928 File Offset: 0x00003B28
		public CalendarGroup Create(string name)
		{
			return base.Create(new CalendarGroup
			{
				Name = name
			}, null);
		}

		// Token: 0x04000054 RID: 84
		private CalendarGroupReference myCalendars;

		// Token: 0x04000055 RID: 85
		private CalendarGroupReference otherCalendars;

		// Token: 0x04000056 RID: 86
		private CalendarGroupDataProvider calendarGroupDataProvider;
	}
}
