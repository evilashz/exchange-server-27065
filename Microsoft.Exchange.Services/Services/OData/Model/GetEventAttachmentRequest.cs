using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F01 RID: 3841
	[AllowedOAuthGrant("Calendars.Read")]
	[AllowedOAuthGrant("Calendars.Write")]
	internal class GetEventAttachmentRequest : GetAttachmentRequest
	{
		// Token: 0x060062B8 RID: 25272 RVA: 0x00134707 File Offset: 0x00132907
		public GetEventAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
