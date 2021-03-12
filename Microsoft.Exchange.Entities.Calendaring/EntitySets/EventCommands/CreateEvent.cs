using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateEvent : CreateSingleEventBase
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006587 File Offset: 0x00004787
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateEventTracer;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000658E File Offset: 0x0000478E
		protected override Event CreateNewEvent()
		{
			return this.Scope.EventDataProvider.Create(base.Entity);
		}
	}
}
