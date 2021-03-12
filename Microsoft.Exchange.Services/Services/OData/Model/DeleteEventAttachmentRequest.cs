using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F07 RID: 3847
	[AllowedOAuthGrant("Calendars.Write")]
	internal class DeleteEventAttachmentRequest : DeleteAttachmentRequest
	{
		// Token: 0x060062C4 RID: 25284 RVA: 0x0013481A File Offset: 0x00132A1A
		public DeleteEventAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
