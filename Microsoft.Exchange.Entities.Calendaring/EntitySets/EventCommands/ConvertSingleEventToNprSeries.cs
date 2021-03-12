using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConvertSingleEventToNprSeries : KeyedEntityCommand<Events, Event>
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000063C6 File Offset: 0x000045C6
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000063CE File Offset: 0x000045CE
		internal IList<Event> AdditionalInstancesToAdd { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000063D7 File Offset: 0x000045D7
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000063DF File Offset: 0x000045DF
		internal string ClientId { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000063E8 File Offset: 0x000045E8
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ConvertSingleEventToNprSeriesTracer;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000063F0 File Offset: 0x000045F0
		protected override Event OnExecute()
		{
			CreateSeriesFromExistingSingleEvent createSeriesFromExistingSingleEvent = new CreateSeriesFromExistingSingleEvent
			{
				SingleEventId = base.EntityKey,
				AdditionalInstancesToAdd = this.AdditionalInstancesToAdd,
				ClientId = this.ClientId,
				Scope = this.Scope
			};
			return createSeriesFromExistingSingleEvent.Execute(this.Context);
		}
	}
}
