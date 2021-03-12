using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F50 RID: 3920
	[AllowedOAuthGrant("Calendars.Write")]
	[AllowedOAuthGrant("Calendars.Read")]
	internal class GetCalendarGroupRequest : GetEntityRequest<CalendarGroup>
	{
		// Token: 0x06006376 RID: 25462 RVA: 0x0013620B File Offset: 0x0013440B
		public GetCalendarGroupRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x00136214 File Offset: 0x00134414
		public override ODataCommand GetODataCommand()
		{
			return new GetCalendarGroupCommand(this);
		}
	}
}
