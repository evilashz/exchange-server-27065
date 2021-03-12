using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F06 RID: 3846
	[AllowedOAuthGrant("Mail.Write")]
	internal class DeleteMessageAttachmentRequest : DeleteAttachmentRequest
	{
		// Token: 0x060062C3 RID: 25283 RVA: 0x00134811 File Offset: 0x00132A11
		public DeleteMessageAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
