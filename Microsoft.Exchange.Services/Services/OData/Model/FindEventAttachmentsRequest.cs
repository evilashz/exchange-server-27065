using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EFB RID: 3835
	[AllowedOAuthGrant("Calendars.Read")]
	[AllowedOAuthGrant("Calendars.Write")]
	internal class FindEventAttachmentsRequest : FindAttachmentsRequest
	{
		// Token: 0x060062AC RID: 25260 RVA: 0x00134600 File Offset: 0x00132800
		public FindEventAttachmentsRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
