using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F02 RID: 3842
	[AllowedOAuthGrant("Contacts.Write")]
	[AllowedOAuthGrant("Contacts.Read")]
	internal class GetContactAttachmentRequest : GetAttachmentRequest
	{
		// Token: 0x060062B9 RID: 25273 RVA: 0x00134710 File Offset: 0x00132910
		public GetContactAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
