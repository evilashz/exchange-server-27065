using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F43 RID: 3907
	internal class GetCalendarCommand : EntityContainersCommand<GetCalendarRequest, GetCalendarResponse>
	{
		// Token: 0x06006358 RID: 25432 RVA: 0x00135C2E File Offset: 0x00133E2E
		public GetCalendarCommand(GetCalendarRequest request) : base(request)
		{
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x00135C38 File Offset: 0x00133E38
		protected override GetCalendarResponse InternalExecute()
		{
			Calendar dataEntityCalendar;
			if (string.Equals(UserSchema.Calendar.Name, base.Request.Id))
			{
				dataEntityCalendar = this.EntityContainers.Calendaring.Calendars.Default.Read(base.CreateCommandContext(null));
			}
			else
			{
				dataEntityCalendar = this.EntityContainers.Calendaring.Calendars.Read(EwsIdConverter.ODataIdToEwsId(base.Request.Id), base.CreateCommandContext(null));
			}
			return new GetCalendarResponse(base.Request)
			{
				Result = GetCalendarCommand.DataEntityCalendarToEntity(dataEntityCalendar, base.Request.ODataQueryOptions)
			};
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x00135CD8 File Offset: 0x00133ED8
		public static Calendar DataEntityCalendarToEntity(Calendar dataEntityCalendar, ODataQueryOptions odataQueryOptions)
		{
			ArgumentValidator.ThrowIfNull("dataEntityEvent", dataEntityCalendar);
			ArgumentValidator.ThrowIfNull("odataQueryOptions", odataQueryOptions);
			Calendar calendar = DataEntityObjectFactory.CreateEntity<Calendar>(dataEntityCalendar);
			QueryAdapter queryAdapter = new DataEntityQueryAdpater(calendar.Schema, odataQueryOptions);
			foreach (PropertyDefinition propertyDefinition in queryAdapter.RequestedProperties)
			{
				propertyDefinition.DataEntityPropertyProvider.GetPropertyFromDataSource(calendar, propertyDefinition, dataEntityCalendar);
			}
			if (odataQueryOptions.Expands(CalendarSchema.Events.Name) && dataEntityCalendar.Events != null)
			{
				List<Event> list = new List<Event>();
				foreach (Event @event in dataEntityCalendar.Events)
				{
					Event event2 = DataEntityObjectFactory.CreateEntity<Event>(@event);
					foreach (PropertyDefinition propertyDefinition2 in event2.Schema.DefaultProperties)
					{
						propertyDefinition2.DataEntityPropertyProvider.GetPropertyFromDataSource(calendar, propertyDefinition2, @event);
					}
					list.Add(event2);
				}
			}
			return calendar;
		}
	}
}
