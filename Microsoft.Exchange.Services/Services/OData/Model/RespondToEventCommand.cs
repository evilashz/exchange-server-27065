using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F3A RID: 3898
	internal class RespondToEventCommand : EntityContainersCommand<RespondToEventRequest, RespondToEventResponse>
	{
		// Token: 0x06006343 RID: 25411 RVA: 0x001357C7 File Offset: 0x001339C7
		public RespondToEventCommand(RespondToEventRequest request) : base(request)
		{
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x001357D0 File Offset: 0x001339D0
		protected override RespondToEventResponse InternalExecute()
		{
			IEvents events = base.GetCalendarContainer(null).Events;
			events.Respond(EwsIdConverter.ODataIdToEwsId(base.Request.Id), base.Request.RespondToEventParameters, base.CreateCommandContext(null));
			return new RespondToEventResponse(base.Request);
		}
	}
}
