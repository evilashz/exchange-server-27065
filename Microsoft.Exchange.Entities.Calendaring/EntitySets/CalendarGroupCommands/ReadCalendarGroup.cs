using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarGroupCommands
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReadCalendarGroup : ReadEntityCommand<CalendarGroups, CalendarGroup>
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005748 File Offset: 0x00003948
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ReadCalendarGroupTracer;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000574F File Offset: 0x0000394F
		protected override CalendarGroup OnExecute()
		{
			return this.Scope.CalendarGroupDataProvider.Read(this.GetEntityStoreId());
		}
	}
}
