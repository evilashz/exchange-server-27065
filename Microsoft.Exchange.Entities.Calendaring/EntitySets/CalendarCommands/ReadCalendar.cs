using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarCommands
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReadCalendar : ReadEntityCommand<Calendars, Calendar>
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000051DB File Offset: 0x000033DB
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ReadCalendarTracer;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000051E4 File Offset: 0x000033E4
		protected override Calendar OnExecute()
		{
			StoreId id = this.Scope.IdConverter.ToStoreObjectId(base.EntityKey);
			return this.Scope.CalendarGroupEntryDataProvider.Read(id);
		}
	}
}
