using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F3B RID: 3899
	[AllowedOAuthGrant("Calendars.Write")]
	internal class UpdateEventRequest : UpdateEntityRequest<Event>
	{
		// Token: 0x06006345 RID: 25413 RVA: 0x0013581D File Offset: 0x00133A1D
		public UpdateEventRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x00135826 File Offset: 0x00133A26
		public override ODataCommand GetODataCommand()
		{
			return new UpdateEventCommand(this);
		}
	}
}
