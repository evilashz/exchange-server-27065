using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F2F RID: 3887
	[AllowedOAuthGrant("Calendars.Read")]
	[AllowedOAuthGrant("Calendars.Write")]
	internal class GetEventRequest : GetEntityRequest<Event>
	{
		// Token: 0x06006325 RID: 25381 RVA: 0x00135320 File Offset: 0x00133520
		public GetEventRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x00135329 File Offset: 0x00133529
		public override ODataCommand GetODataCommand()
		{
			return new GetEventCommand(this);
		}
	}
}
