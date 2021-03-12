using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeleteEvent : DeleteEventBase
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000758F File Offset: 0x0000578F
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00007597 File Offset: 0x00005797
		internal IEventCommandFactory CommandFactory { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000075A0 File Offset: 0x000057A0
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.DeleteEventTracer;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000075A7 File Offset: 0x000057A7
		protected override void DeleteCancelledEventFromAttendeesCalendar(Event eventToDelete)
		{
			this.Scope.EventDataProvider.Delete(this.Scope.IdConverter.ToStoreObjectId(eventToDelete.Id), DeleteItemFlags.MoveToDeletedItems);
		}
	}
}
