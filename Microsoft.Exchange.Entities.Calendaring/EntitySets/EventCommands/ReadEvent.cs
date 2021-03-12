using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReadEvent : ReadEntityCommand<Events, Event>
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008854 File Offset: 0x00006A54
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ReadEventTracer;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000885C File Offset: 0x00006A5C
		protected override Event OnExecute()
		{
			StoreId entityStoreId = this.GetEntityStoreId();
			EventDataProvider dataProvider = this.Scope.GetDataProvider(entityStoreId);
			Event @event = dataProvider.Read(entityStoreId);
			this.Scope.TimeAdjuster.AdjustTimeProperties(@event, this.Scope.Session.ExTimeZone);
			return @event;
		}
	}
}
