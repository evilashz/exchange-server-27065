using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F52 RID: 3922
	internal class GetCalendarGroupCommand : EntityContainersCommand<GetCalendarGroupRequest, GetCalendarGroupResponse>
	{
		// Token: 0x06006379 RID: 25465 RVA: 0x00136225 File Offset: 0x00134425
		public GetCalendarGroupCommand(GetCalendarGroupRequest request) : base(request)
		{
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x00136230 File Offset: 0x00134430
		protected override GetCalendarGroupResponse InternalExecute()
		{
			CalendarGroup dataEntityCalendarGroup = this.EntityContainers.Calendaring.CalendarGroups.Read(EwsIdConverter.ODataIdToEwsId(base.Request.Id), base.CreateCommandContext(null));
			return new GetCalendarGroupResponse(base.Request)
			{
				Result = GetCalendarGroupCommand.DataEntityCalendarGroupToEntity(dataEntityCalendarGroup, base.Request.ODataQueryOptions)
			};
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x00136290 File Offset: 0x00134490
		public static CalendarGroup DataEntityCalendarGroupToEntity(CalendarGroup dataEntityCalendarGroup, ODataQueryOptions odataQueryOptions)
		{
			ArgumentValidator.ThrowIfNull("dataEntityCalendarGroup", dataEntityCalendarGroup);
			ArgumentValidator.ThrowIfNull("odataQueryOptions", odataQueryOptions);
			CalendarGroup calendarGroup = DataEntityObjectFactory.CreateEntity<CalendarGroup>(dataEntityCalendarGroup);
			QueryAdapter queryAdapter = new DataEntityQueryAdpater(CalendarGroupSchema.SchemaInstance, odataQueryOptions);
			foreach (PropertyDefinition propertyDefinition in queryAdapter.RequestedProperties)
			{
				propertyDefinition.DataEntityPropertyProvider.GetPropertyFromDataSource(calendarGroup, propertyDefinition, dataEntityCalendarGroup);
			}
			return calendarGroup;
		}
	}
}
