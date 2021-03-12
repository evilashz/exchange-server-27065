using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F35 RID: 3893
	[AllowedOAuthGrant("Calendars.Write")]
	internal class DeleteEventRequest : DeleteEntityRequest<Event>
	{
		// Token: 0x06006335 RID: 25397 RVA: 0x0013564A File Offset: 0x0013384A
		public DeleteEventRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00135653 File Offset: 0x00133853
		public override ODataCommand GetODataCommand()
		{
			return new DeleteEventCommand(this);
		}
	}
}
