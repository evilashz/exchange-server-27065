using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarGroupCommands
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FindCalendarGroups : FindEntitiesCommand<CalendarGroups, CalendarGroup>
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005310 File Offset: 0x00003510
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.FindCalendarGroupsTracer;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000055B8 File Offset: 0x000037B8
		protected IEnumerable<CalendarGroup> FindAllCalendarGroups()
		{
			bool expandCalendars = base.ShouldExpand("Calendars");
			foreach (CalendarGroupInfo group in this.GetCalendarGroupInfoList(expandCalendars))
			{
				CalendarGroup entity = this.Scope.IdConverter.CreateBasicEntity<CalendarGroup>(group.Id, this.Scope.StoreSession);
				entity.Name = group.GroupName;
				entity.ClassId = group.GroupClassId;
				if (expandCalendars)
				{
					entity.Calendars = from calendarInfo in @group.Calendars
					where calendarInfo is LocalCalendarGroupEntryInfo
					select this.ConvertCalendar(calendarInfo);
				}
				yield return entity;
			}
			yield break;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000055D8 File Offset: 0x000037D8
		protected override IEnumerable<CalendarGroup> OnExecute()
		{
			IEnumerable<CalendarGroup> source = this.FindAllCalendarGroups();
			return base.QueryOptions.ApplyTo(source.AsQueryable<CalendarGroup>());
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005600 File Offset: 0x00003800
		private Calendar ConvertCalendar(CalendarGroupEntryInfo calendarInfo)
		{
			Calendar calendar = this.Scope.IdConverter.CreateBasicEntity<Calendar>(calendarInfo.Id, this.Scope.StoreSession);
			calendar.CalendarFolderStoreId = calendarInfo.CalendarId;
			calendar.Name = calendarInfo.CalendarName;
			calendar.Color = calendarInfo.CalendarColor;
			return calendar;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005654 File Offset: 0x00003854
		private IEnumerable<CalendarGroupInfo> GetCalendarGroupInfoList(bool ensureCalendarInfoIsLoaded)
		{
			bool flag = false;
			int num = 0;
			CalendarGroupInfoList calendarGroupsView;
			do
			{
				num++;
				calendarGroupsView = this.Scope.XsoFactory.GetCalendarGroupsView(this.Scope.StoreSession);
				if (!calendarGroupsView.DefaultGroups.ContainsKey(CalendarGroupType.MyCalendars))
				{
					using (ICalendarGroup calendarGroup = this.Scope.XsoFactory.BindToCalendarGroup(this.Scope.StoreSession, CalendarGroupType.MyCalendars))
					{
						calendarGroupsView.Add(calendarGroup.GetCalendarGroupInfo());
						flag = true;
					}
				}
				if (!calendarGroupsView.DefaultGroups.ContainsKey(CalendarGroupType.OtherCalendars))
				{
					using (ICalendarGroup calendarGroup2 = this.Scope.XsoFactory.BindToCalendarGroup(this.Scope.StoreSession, CalendarGroupType.OtherCalendars))
					{
						calendarGroupsView.Add(calendarGroup2.GetCalendarGroupInfo());
						flag = true;
					}
				}
			}
			while (ensureCalendarInfoIsLoaded && flag && num < 2);
			return calendarGroupsView;
		}
	}
}
