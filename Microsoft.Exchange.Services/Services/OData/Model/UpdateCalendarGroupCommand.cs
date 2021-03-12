using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F5B RID: 3931
	internal class UpdateCalendarGroupCommand : EntityContainersCommand<UpdateCalendarGroupRequest, UpdateCalendarGroupResponse>
	{
		// Token: 0x0600638A RID: 25482 RVA: 0x00136460 File Offset: 0x00134660
		public UpdateCalendarGroupCommand(UpdateCalendarGroupRequest request) : base(request)
		{
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x0013646C File Offset: 0x0013466C
		protected override UpdateCalendarGroupResponse InternalExecute()
		{
			CalendarGroup entity = DataEntityObjectFactory.CreateAndSetPropertiesOnDataEntityForUpdate<CalendarGroup>(base.Request.Change);
			string key = EwsIdConverter.ODataIdToEwsId(base.Request.Id);
			CalendarGroup dataEntityCalendarGroup = this.EntityContainers.Calendaring.CalendarGroups.Update(key, entity, base.CreateCommandContext(null));
			return new UpdateCalendarGroupResponse(base.Request)
			{
				Result = GetCalendarGroupCommand.DataEntityCalendarGroupToEntity(dataEntityCalendarGroup, base.Request.ODataQueryOptions)
			};
		}
	}
}
