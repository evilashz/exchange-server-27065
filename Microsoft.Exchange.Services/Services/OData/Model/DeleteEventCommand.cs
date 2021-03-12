using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F37 RID: 3895
	internal class DeleteEventCommand : EntityContainersCommand<DeleteEventRequest, DeleteEventResponse>
	{
		// Token: 0x06006338 RID: 25400 RVA: 0x00135664 File Offset: 0x00133864
		public DeleteEventCommand(DeleteEventRequest request) : base(request)
		{
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x00135670 File Offset: 0x00133870
		protected override DeleteEventResponse InternalExecute()
		{
			IEvents events = base.GetCalendarContainer(null).Events;
			events.Delete(EwsIdConverter.ODataIdToEwsId(base.Request.Id), base.CreateCommandContext(null));
			return new DeleteEventResponse(base.Request);
		}
	}
}
