using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarGroupCommands
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UpdateCalendarGroup : UpdateStorageEntityCommand<CalendarGroups, CalendarGroup>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000576F File Offset: 0x0000396F
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.UpdateCalendarGroupTracer;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005776 File Offset: 0x00003976
		protected override CalendarGroup OnExecute()
		{
			return this.Scope.CalendarGroupDataProvider.Update(base.Entity, this.Context);
		}
	}
}
