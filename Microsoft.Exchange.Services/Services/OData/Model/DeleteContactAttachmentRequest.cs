using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F08 RID: 3848
	[AllowedOAuthGrant("Contacts.Write")]
	internal class DeleteContactAttachmentRequest : DeleteAttachmentRequest
	{
		// Token: 0x060062C5 RID: 25285 RVA: 0x00134823 File Offset: 0x00132A23
		public DeleteContactAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
