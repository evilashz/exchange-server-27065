using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EE3 RID: 3811
	[AllowedOAuthGrant("Mail.Write")]
	[AllowedOAuthGrant("Mail.Read")]
	internal class GetMessageRequest : GetEntityRequest<Message>
	{
		// Token: 0x06006278 RID: 25208 RVA: 0x00133FFA File Offset: 0x001321FA
		public GetMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x00134003 File Offset: 0x00132203
		public override ODataCommand GetODataCommand()
		{
			return new GetMessageCommand(this);
		}
	}
}
