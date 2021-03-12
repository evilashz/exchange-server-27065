using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F0D RID: 3853
	[AllowedOAuthGrant("Calendars.Write")]
	internal class CreateEventAttachmentRequest : CreateAttachmentRequest
	{
		// Token: 0x060062D2 RID: 25298 RVA: 0x00134939 File Offset: 0x00132B39
		public CreateEventAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x060062D3 RID: 25299 RVA: 0x00134942 File Offset: 0x00132B42
		protected override string ParentItemNavigationName
		{
			get
			{
				return UserSchema.Events.Name;
			}
		}
	}
}
