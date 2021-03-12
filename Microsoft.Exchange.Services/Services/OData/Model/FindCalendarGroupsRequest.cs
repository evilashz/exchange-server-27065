using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F53 RID: 3923
	[AllowedOAuthGrant("Calendars.Write")]
	[AllowedOAuthGrant("Calendars.Read")]
	internal class FindCalendarGroupsRequest : FindEntitiesRequest<CalendarGroup>
	{
		// Token: 0x0600637C RID: 25468 RVA: 0x00136310 File Offset: 0x00134510
		public FindCalendarGroupsRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x00136319 File Offset: 0x00134519
		public override ODataCommand GetODataCommand()
		{
			return new FindCalendarGroupsCommand(this);
		}
	}
}
