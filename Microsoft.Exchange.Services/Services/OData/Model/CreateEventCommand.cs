using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F34 RID: 3892
	internal class CreateEventCommand : EntityContainersCommand<CreateEventRequest, CreateEventResponse>
	{
		// Token: 0x06006333 RID: 25395 RVA: 0x001355D1 File Offset: 0x001337D1
		public CreateEventCommand(CreateEventRequest request) : base(request)
		{
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x001355DC File Offset: 0x001337DC
		protected override CreateEventResponse InternalExecute()
		{
			Event entity = DataEntityObjectFactory.CreateAndSetPropertiesOnDataEntityForCreate<Event>(base.Request.Template);
			IEvents events = base.GetCalendarContainer(base.Request.CalendarId).Events;
			Event dataEntityEvent = events.Create(entity, base.CreateCommandContext(null));
			return new CreateEventResponse(base.Request)
			{
				Result = GetEventCommand.DataEntityEventToEntity(dataEntityEvent, base.Request.ODataQueryOptions, base.ExchangeService)
			};
		}
	}
}
