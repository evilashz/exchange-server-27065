using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.CalendarGroupCommands
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeleteCalendarGroup : DeleteEntityCommand<CalendarGroups>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000052D3 File Offset: 0x000034D3
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.DeleteCalendarGroupTracer;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000052DA File Offset: 0x000034DA
		protected override VoidResult OnExecute()
		{
			this.Scope.CalendarGroupDataProvider.Delete(this.Scope.IdConverter.ToStoreObjectId(base.EntityKey), DeleteItemFlags.HardDelete);
			return VoidResult.Value;
		}
	}
}
