using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F00 RID: 3840
	[AllowedOAuthGrant("Mail.Read")]
	[AllowedOAuthGrant("Mail.Write")]
	internal class GetMessageAttachmentRequest : GetAttachmentRequest
	{
		// Token: 0x060062B7 RID: 25271 RVA: 0x001346FE File Offset: 0x001328FE
		public GetMessageAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
