using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarGroupCommands
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateCalendarGroup : CreateEntityCommand<CalendarGroups, CalendarGroup>
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000052AC File Offset: 0x000034AC
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateCalendarGroupCallTracer;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052B3 File Offset: 0x000034B3
		protected override CalendarGroup OnExecute()
		{
			return this.Scope.CalendarGroupDataProvider.Create(base.Entity);
		}
	}
}
