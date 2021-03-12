using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F4C RID: 3916
	internal class DeleteCalendarCommand : EntityContainersCommand<DeleteCalendarRequest, DeleteCalendarResponse>
	{
		// Token: 0x0600636F RID: 25455 RVA: 0x0013611C File Offset: 0x0013431C
		public DeleteCalendarCommand(DeleteCalendarRequest request) : base(request)
		{
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x00136128 File Offset: 0x00134328
		protected override DeleteCalendarResponse InternalExecute()
		{
			string key = EwsIdConverter.ODataIdToEwsId(base.Request.Id);
			this.EntityContainers.Calendaring.Calendars.Delete(key, base.CreateCommandContext(null));
			return new DeleteCalendarResponse(base.Request);
		}
	}
}
