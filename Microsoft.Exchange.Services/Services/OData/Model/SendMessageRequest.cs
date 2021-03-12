using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F17 RID: 3863
	[AllowedOAuthGrant("Mail.Send")]
	internal class SendMessageRequest : EntityActionRequest<Message>
	{
		// Token: 0x060062F0 RID: 25328 RVA: 0x00134C24 File Offset: 0x00132E24
		public SendMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x00134C2D File Offset: 0x00132E2D
		public override ODataCommand GetODataCommand()
		{
			return new SendMessageCommand(this);
		}
	}
}
