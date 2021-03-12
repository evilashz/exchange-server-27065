using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F5E RID: 3934
	internal class DeleteCalendarGroupCommand : EntityContainersCommand<DeleteCalendarGroupRequest, DeleteCalendarGroupResponse>
	{
		// Token: 0x0600638F RID: 25487 RVA: 0x001364FD File Offset: 0x001346FD
		public DeleteCalendarGroupCommand(DeleteCalendarGroupRequest request) : base(request)
		{
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x00136508 File Offset: 0x00134708
		protected override DeleteCalendarGroupResponse InternalExecute()
		{
			string key = EwsIdConverter.ODataIdToEwsId(base.Request.Id);
			this.EntityContainers.Calendaring.CalendarGroups.Delete(key, base.CreateCommandContext(null));
			return new DeleteCalendarGroupResponse(base.Request);
		}
	}
}
