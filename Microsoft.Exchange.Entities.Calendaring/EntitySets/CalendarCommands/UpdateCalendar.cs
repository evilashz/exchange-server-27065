using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarCommands
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UpdateCalendar : UpdateStorageEntityCommand<Calendars, Calendar>
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005221 File Offset: 0x00003421
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.UpdateCalendarTracer;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005228 File Offset: 0x00003428
		protected override Calendar OnExecute()
		{
			CalendarGroupEntryDataProvider calendarGroupEntryDataProvider = this.Scope.CalendarGroupEntryDataProvider;
			Calendar result;
			using (ICalendarGroupEntry calendarGroupEntry = calendarGroupEntryDataProvider.ValidateAndBindToWrite(base.Entity))
			{
				if (calendarGroupEntry.IsLocalMailboxCalendar)
				{
					this.Scope.CalendarFolderDataProvider.UpdateOnly(base.Entity, calendarGroupEntry.CalendarId);
				}
				result = calendarGroupEntryDataProvider.Update(base.Entity, calendarGroupEntry, this.Context);
			}
			return result;
		}
	}
}
