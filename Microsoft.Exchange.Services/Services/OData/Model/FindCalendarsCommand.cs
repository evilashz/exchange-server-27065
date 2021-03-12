using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F46 RID: 3910
	internal class FindCalendarsCommand : EntityContainersCommand<FindCalendarsRequest, FindCalendarsResponse>
	{
		// Token: 0x06006361 RID: 25441 RVA: 0x00135EB3 File Offset: 0x001340B3
		public FindCalendarsCommand(FindCalendarsRequest request) : base(request)
		{
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x00135ED0 File Offset: 0x001340D0
		protected override FindCalendarsResponse InternalExecute()
		{
			DataEntityQueryAdpater dataEntityQueryAdpater = new DataEntityQueryAdpater(CalendarSchema.SchemaInstance, base.Request.ODataQueryOptions);
			IEnumerable<Calendar> source;
			if (string.IsNullOrEmpty(base.Request.CalendarGroupId))
			{
				source = this.EntityContainers.Calendaring.Calendars.Find(dataEntityQueryAdpater.GetEntityQueryOptions(), base.CreateCommandContext(null));
			}
			else
			{
				string calendarGroupId = EwsIdConverter.ODataIdToEwsId(base.Request.CalendarGroupId);
				source = this.EntityContainers.Calendaring.CalendarGroups[calendarGroupId].Calendars.Find(dataEntityQueryAdpater.GetEntityQueryOptions(), base.CreateCommandContext(null));
			}
			IEnumerable<Calendar> entities = (from x in source
			select GetCalendarCommand.DataEntityCalendarToEntity(x, base.Request.ODataQueryOptions)).ToList<Calendar>();
			return new FindCalendarsResponse(base.Request)
			{
				Result = new FindEntitiesResult<Calendar>(entities, -1)
			};
		}
	}
}
