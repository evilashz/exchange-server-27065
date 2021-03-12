using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F56 RID: 3926
	[AllowedOAuthGrant("Calendars.Write")]
	internal class CreateCalendarGroupRequest : CreateEntityRequest<CalendarGroup>
	{
		// Token: 0x06006382 RID: 25474 RVA: 0x001363C0 File Offset: 0x001345C0
		public CreateCalendarGroupRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x001363C9 File Offset: 0x001345C9
		public override ODataCommand GetODataCommand()
		{
			return new CreateCalendarGroupCommand(this);
		}
	}
}
