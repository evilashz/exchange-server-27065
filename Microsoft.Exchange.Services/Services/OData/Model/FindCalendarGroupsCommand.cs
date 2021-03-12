using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F55 RID: 3925
	internal class FindCalendarGroupsCommand : EntityContainersCommand<FindCalendarGroupsRequest, FindCalendarGroupsResponse>
	{
		// Token: 0x0600637F RID: 25471 RVA: 0x0013632A File Offset: 0x0013452A
		public FindCalendarGroupsCommand(FindCalendarGroupsRequest request) : base(request)
		{
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x00136348 File Offset: 0x00134548
		protected override FindCalendarGroupsResponse InternalExecute()
		{
			DataEntityQueryAdpater dataEntityQueryAdpater = new DataEntityQueryAdpater(CalendarGroupSchema.SchemaInstance, base.Request.ODataQueryOptions);
			IEnumerable<CalendarGroup> source = this.EntityContainers.Calendaring.CalendarGroups.Find(dataEntityQueryAdpater.GetEntityQueryOptions(), base.CreateCommandContext(null));
			IEnumerable<CalendarGroup> entities = (from x in source
			select GetCalendarGroupCommand.DataEntityCalendarGroupToEntity(x, base.Request.ODataQueryOptions)).ToList<CalendarGroup>();
			return new FindCalendarGroupsResponse(base.Request)
			{
				Result = new FindEntitiesResult<CalendarGroup>(entities, -1)
			};
		}
	}
}
