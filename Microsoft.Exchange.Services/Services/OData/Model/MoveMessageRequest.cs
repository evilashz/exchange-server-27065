using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF3 RID: 3827
	[AllowedOAuthGrant("Mail.Write")]
	internal class MoveMessageRequest : CopyOrMoveEntityRequest<Message>
	{
		// Token: 0x0600629B RID: 25243 RVA: 0x0013447A File Offset: 0x0013267A
		public MoveMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x00134483 File Offset: 0x00132683
		public override ODataCommand GetODataCommand()
		{
			return new MoveMessageCommand(this);
		}
	}
}
