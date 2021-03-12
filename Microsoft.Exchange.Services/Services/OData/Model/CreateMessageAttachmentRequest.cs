using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F0C RID: 3852
	[AllowedOAuthGrant("Mail.Write")]
	internal class CreateMessageAttachmentRequest : CreateAttachmentRequest
	{
		// Token: 0x060062D0 RID: 25296 RVA: 0x00134924 File Offset: 0x00132B24
		public CreateMessageAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x060062D1 RID: 25297 RVA: 0x0013492D File Offset: 0x00132B2D
		protected override string ParentItemNavigationName
		{
			get
			{
				return UserSchema.Messages.Name;
			}
		}
	}
}
