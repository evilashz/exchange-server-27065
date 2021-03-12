using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x0200002E RID: 46
	internal class CalendarGroupReference : StorageEntityReference<CalendarGroups, CalendarGroup, IMailboxSession>, ICalendarGroupReference, IEntityReference<CalendarGroup>
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000579C File Offset: 0x0000399C
		public CalendarGroupReference(CalendarGroups calendarGroups, string calendarGroupKey) : base(calendarGroups, calendarGroupKey)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057A6 File Offset: 0x000039A6
		public CalendarGroupReference(CalendarGroups calendarGroups, StoreId calendarGroupStoreId) : base(calendarGroups, calendarGroupStoreId)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000057B0 File Offset: 0x000039B0
		public CalendarGroupReference(CalendarGroups calendarGroups, CalendarGroupType calendarGroupType) : base(calendarGroups)
		{
			this.calendarGroupType = new CalendarGroupType?(calendarGroupType);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000057C8 File Offset: 0x000039C8
		public ICalendars Calendars
		{
			get
			{
				ICalendars result;
				if ((result = this.calendars) == null)
				{
					result = (this.calendars = new CalendarsInCalendarGroup(this));
				}
				return result;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000057F0 File Offset: 0x000039F0
		protected override StoreId ResolveReference()
		{
			StoreId id;
			using (ICalendarGroup calendarGroup = base.XsoFactory.BindToCalendarGroup(base.StoreSession, this.calendarGroupType.Value))
			{
				id = calendarGroup.Id;
			}
			return id;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005840 File Offset: 0x00003A40
		protected override string GetRelativeDescription()
		{
			if (this.calendarGroupType != null)
			{
				return '.' + this.calendarGroupType.Value.ToString();
			}
			return base.GetRelativeDescription();
		}

		// Token: 0x04000052 RID: 82
		private CalendarGroupType? calendarGroupType;

		// Token: 0x04000053 RID: 83
		private ICalendars calendars;
	}
}
