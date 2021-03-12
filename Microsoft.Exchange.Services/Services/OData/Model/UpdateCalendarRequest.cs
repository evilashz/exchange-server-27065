using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F4D RID: 3917
	[AllowedOAuthGrant("Calendars.Write")]
	internal class UpdateCalendarRequest : UpdateEntityRequest<Calendar>
	{
		// Token: 0x06006371 RID: 25457 RVA: 0x0013616E File Offset: 0x0013436E
		public UpdateCalendarRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x00136177 File Offset: 0x00134377
		public override ODataCommand GetODataCommand()
		{
			return new UpdateCalendarCommand(this);
		}
	}
}
