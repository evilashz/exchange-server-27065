using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EFC RID: 3836
	[AllowedOAuthGrant("Contacts.Write")]
	[AllowedOAuthGrant("Contacts.Read")]
	internal class FindContactAttachmentsRequest : FindAttachmentsRequest
	{
		// Token: 0x060062AD RID: 25261 RVA: 0x00134609 File Offset: 0x00132809
		public FindContactAttachmentsRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
