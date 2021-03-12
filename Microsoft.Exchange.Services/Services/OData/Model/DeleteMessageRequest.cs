using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EED RID: 3821
	[AllowedOAuthGrant("Mail.Write")]
	internal class DeleteMessageRequest : DeleteEntityRequest<Message>
	{
		// Token: 0x06006291 RID: 25233 RVA: 0x001343B5 File Offset: 0x001325B5
		public DeleteMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x001343BE File Offset: 0x001325BE
		public override ODataCommand GetODataCommand()
		{
			return new DeleteMessageCommand(this);
		}
	}
}
