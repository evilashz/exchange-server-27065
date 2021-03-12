using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateSeries : CreateEventBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007274 File Offset: 0x00005474
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateSeriesTracer;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000727C File Offset: 0x0000547C
		protected override Event OnExecute()
		{
			this.ValidateParameters();
			CreateNewSeries createNewSeries = new CreateNewSeries
			{
				Entity = base.Entity,
				Scope = this.Scope,
				ClientId = base.Entity.ClientId
			};
			return createNewSeries.Execute(this.Context);
		}
	}
}
