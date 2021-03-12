using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarCommands
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FindCalendars : FindEntitiesCommand<Calendars, Calendar>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000050C8 File Offset: 0x000032C8
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.FindCalendarsTracer;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000050D0 File Offset: 0x000032D0
		protected override IEnumerable<Calendar> OnExecute()
		{
			IEnumerable<Calendar> source = this.FindAllCalendars();
			return base.QueryOptions.ApplyTo(source.AsQueryable<Calendar>());
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005124 File Offset: 0x00003324
		private IEnumerable<Calendar> FindAllCalendars()
		{
			CommandContext commandContext = new CommandContext();
			commandContext.Expand = new string[]
			{
				"Calendars"
			};
			CalendaringContainer calendaringContainer = new CalendaringContainer(this.Scope);
			IEnumerable<CalendarGroup> source = calendaringContainer.CalendarGroups.AsQueryable(commandContext).AsEnumerable<CalendarGroup>();
			CalendarsInCalendarGroup calendarsInCalendarGroup = this.Scope as CalendarsInCalendarGroup;
			if (calendarsInCalendarGroup != null)
			{
				source = (from c in source
				where c.Id == calendarsInCalendarGroup.CalendarGroup.GetKey()
				select c).ToList<CalendarGroup>();
			}
			return source.SelectMany((CalendarGroup calendarGroup) => calendarGroup.Calendars);
		}
	}
}
