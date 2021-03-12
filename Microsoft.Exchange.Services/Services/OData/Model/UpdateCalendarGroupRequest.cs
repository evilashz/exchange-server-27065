using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F59 RID: 3929
	[AllowedOAuthGrant("Calendars.Write")]
	internal class UpdateCalendarGroupRequest : UpdateEntityRequest<CalendarGroup>
	{
		// Token: 0x06006387 RID: 25479 RVA: 0x00136446 File Offset: 0x00134646
		public UpdateCalendarGroupRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0013644F File Offset: 0x0013464F
		public override ODataCommand GetODataCommand()
		{
			return new UpdateCalendarGroupCommand(this);
		}
	}
}
