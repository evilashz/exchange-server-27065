using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF6 RID: 3830
	[AllowedOAuthGrant("Mail.Write")]
	internal class UpdateMessageRequest : UpdateEntityRequest<Message>
	{
		// Token: 0x060062A0 RID: 25248 RVA: 0x001344EA File Offset: 0x001326EA
		public UpdateMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x001344F3 File Offset: 0x001326F3
		public override ODataCommand GetODataCommand()
		{
			return new UpdateMessageCommand(this);
		}
	}
}
