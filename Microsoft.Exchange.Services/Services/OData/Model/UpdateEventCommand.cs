using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F3D RID: 3901
	internal class UpdateEventCommand : EntityContainersCommand<UpdateEventRequest, UpdateEventResponse>
	{
		// Token: 0x06006348 RID: 25416 RVA: 0x00135837 File Offset: 0x00133A37
		public UpdateEventCommand(UpdateEventRequest request) : base(request)
		{
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x00135840 File Offset: 0x00133A40
		protected override UpdateEventResponse InternalExecute()
		{
			Event entity = DataEntityObjectFactory.CreateAndSetPropertiesOnDataEntityForUpdate<Event>(base.Request.Change);
			IEvents events = base.GetCalendarContainer(null).Events;
			Event dataEntityEvent = events.Update(EwsIdConverter.ODataIdToEwsId(base.Request.Id), entity, base.CreateCommandContext(null));
			return new UpdateEventResponse(base.Request)
			{
				Result = GetEventCommand.DataEntityEventToEntity(dataEntityEvent, base.Request.ODataQueryOptions, base.ExchangeService)
			};
		}
	}
}
