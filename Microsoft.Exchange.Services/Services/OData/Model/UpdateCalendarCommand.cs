using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F4F RID: 3919
	internal class UpdateCalendarCommand : EntityContainersCommand<UpdateCalendarRequest, UpdateCalendarResponse>
	{
		// Token: 0x06006374 RID: 25460 RVA: 0x00136188 File Offset: 0x00134388
		public UpdateCalendarCommand(UpdateCalendarRequest request) : base(request)
		{
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x00136194 File Offset: 0x00134394
		protected override UpdateCalendarResponse InternalExecute()
		{
			Calendar entity = DataEntityObjectFactory.CreateAndSetPropertiesOnDataEntityForUpdate<Calendar>(base.Request.Change);
			string key = EwsIdConverter.ODataIdToEwsId(base.Request.Id);
			Calendar dataEntityCalendar = this.EntityContainers.Calendaring.Calendars.Update(key, entity, base.CreateCommandContext(null));
			return new UpdateCalendarResponse(base.Request)
			{
				Result = GetCalendarCommand.DataEntityCalendarToEntity(dataEntityCalendar, base.Request.ODataQueryOptions)
			};
		}
	}
}
