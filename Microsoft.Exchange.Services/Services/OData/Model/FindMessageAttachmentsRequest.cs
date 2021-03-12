using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EFA RID: 3834
	[AllowedOAuthGrant("Mail.Read")]
	[AllowedOAuthGrant("Mail.Write")]
	internal class FindMessageAttachmentsRequest : FindAttachmentsRequest
	{
		// Token: 0x060062AB RID: 25259 RVA: 0x001345F7 File Offset: 0x001327F7
		public FindMessageAttachmentsRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
