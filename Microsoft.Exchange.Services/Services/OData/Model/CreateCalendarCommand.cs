using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F49 RID: 3913
	internal class CreateCalendarCommand : EntityContainersCommand<CreateCalendarRequest, CreateCalendarResponse>
	{
		// Token: 0x0600636A RID: 25450 RVA: 0x0013604A File Offset: 0x0013424A
		public CreateCalendarCommand(CreateCalendarRequest request) : base(request)
		{
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x00136054 File Offset: 0x00134254
		protected override CreateCalendarResponse InternalExecute()
		{
			Calendar entity = DataEntityObjectFactory.CreateAndSetPropertiesOnDataEntityForCreate<Calendar>(base.Request.Template);
			Calendar dataEntityCalendar;
			if (base.Request.CalendarGroupId == null)
			{
				dataEntityCalendar = this.EntityContainers.Calendaring.Calendars.Create(entity, base.CreateCommandContext(null));
			}
			else
			{
				string calendarGroupId = EwsIdConverter.ODataIdToEwsId(base.Request.CalendarGroupId);
				dataEntityCalendar = this.EntityContainers.Calendaring.CalendarGroups[calendarGroupId].Calendars.Create(entity, base.CreateCommandContext(null));
			}
			return new CreateCalendarResponse(base.Request)
			{
				Result = GetCalendarCommand.DataEntityCalendarToEntity(dataEntityCalendar, base.Request.ODataQueryOptions)
			};
		}
	}
}
