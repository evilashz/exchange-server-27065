using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF0 RID: 3824
	[AllowedOAuthGrant("Mail.Write")]
	internal class CopyMessageRequest : CopyOrMoveEntityRequest<Message>
	{
		// Token: 0x06006296 RID: 25238 RVA: 0x0013440D File Offset: 0x0013260D
		public CopyMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x00134416 File Offset: 0x00132616
		public override ODataCommand GetODataCommand()
		{
			return new CopyMessageCommand(this);
		}
	}
}
