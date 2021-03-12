using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F0E RID: 3854
	[AllowedOAuthGrant("Contacts.Write")]
	internal class CreateContactAttachmentRequest : CreateAttachmentRequest
	{
		// Token: 0x060062D4 RID: 25300 RVA: 0x0013494E File Offset: 0x00132B4E
		public CreateContactAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x00134957 File Offset: 0x00132B57
		protected override string ParentItemNavigationName
		{
			get
			{
				return UserSchema.Contacts.Name;
			}
		}
	}
}
