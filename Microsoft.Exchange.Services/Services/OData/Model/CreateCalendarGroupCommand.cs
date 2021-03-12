using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F58 RID: 3928
	internal class CreateCalendarGroupCommand : EntityContainersCommand<CreateCalendarGroupRequest, CreateCalendarGroupResponse>
	{
		// Token: 0x06006385 RID: 25477 RVA: 0x001363DA File Offset: 0x001345DA
		public CreateCalendarGroupCommand(CreateCalendarGroupRequest request) : base(request)
		{
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x001363E4 File Offset: 0x001345E4
		protected override CreateCalendarGroupResponse InternalExecute()
		{
			CalendarGroup entity = DataEntityObjectFactory.CreateAndSetPropertiesOnDataEntityForCreate<CalendarGroup>(base.Request.Template);
			CalendarGroup dataEntityCalendarGroup = this.EntityContainers.Calendaring.CalendarGroups.Create(entity, base.CreateCommandContext(null));
			return new CreateCalendarGroupResponse(base.Request)
			{
				Result = GetCalendarGroupCommand.DataEntityCalendarGroupToEntity(dataEntityCalendarGroup, base.Request.ODataQueryOptions)
			};
		}
	}
}
