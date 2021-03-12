using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarCommands
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeleteCalendar : DeleteEntityCommand<Calendars>
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005008 File Offset: 0x00003208
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.DeleteCalendarTracer;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005010 File Offset: 0x00003210
		protected override VoidResult OnExecute()
		{
			CalendarGroupEntryDataProvider calendarGroupEntryDataProvider = this.Scope.CalendarGroupEntryDataProvider;
			using (ICalendarGroupEntry calendarGroupEntry = calendarGroupEntryDataProvider.Bind(this.Scope.IdConverter.ToStoreObjectId(base.EntityKey)))
			{
				bool isLocalMailboxCalendar = calendarGroupEntry.IsLocalMailboxCalendar;
				if (isLocalMailboxCalendar)
				{
					this.Scope.CalendarFolderDataProvider.Delete(calendarGroupEntry.CalendarId, DeleteItemFlags.MoveToDeletedItems);
				}
				try
				{
					calendarGroupEntryDataProvider.Delete(calendarGroupEntry.Id, DeleteItemFlags.HardDelete);
				}
				catch (StoragePermanentException arg)
				{
					if (!isLocalMailboxCalendar)
					{
						throw;
					}
					this.Trace.TraceError<DeleteCalendar, StoragePermanentException>(0L, "{0}. Local calendar folder was moved to deleted items but calendar group entry failed to be deleted. {1}", this, arg);
				}
			}
			return VoidResult.Value;
		}
	}
}
