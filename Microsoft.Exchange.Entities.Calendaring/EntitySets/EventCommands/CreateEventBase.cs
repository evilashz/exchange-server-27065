using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CreateEventBase : CreateEntityCommand<Events, Event>
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00006449 File Offset: 0x00004649
		protected virtual void ValidateSeriesId()
		{
			if (base.Entity.IsPropertySet(base.Entity.Schema.SeriesIdProperty))
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorCallerCantSpecifySeriesId);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006473 File Offset: 0x00004673
		protected virtual void ValidateParameters()
		{
			if (base.Entity.IsPropertySet(base.Entity.Schema.SeriesMasterIdProperty))
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorCallerCantSpecifySeriesMasterId);
			}
			this.ValidateSeriesId();
		}
	}
}
