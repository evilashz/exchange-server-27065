using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F41 RID: 3905
	[AllowedOAuthGrant("Calendars.Write")]
	[AllowedOAuthGrant("Calendars.Read")]
	internal class GetCalendarRequest : GetEntityRequest<Calendar>
	{
		// Token: 0x06006355 RID: 25429 RVA: 0x00135C14 File Offset: 0x00133E14
		public GetCalendarRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x00135C1D File Offset: 0x00133E1D
		public override ODataCommand GetODataCommand()
		{
			return new GetCalendarCommand(this);
		}
	}
}
