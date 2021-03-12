using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F1A RID: 3866
	[AllowedOAuthGrant("Contacts.Write")]
	internal class CreateContactRequest : CreateEntityRequest<Contact>
	{
		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x00134C85 File Offset: 0x00132E85
		// (set) Token: 0x060062F7 RID: 25335 RVA: 0x00134C8D File Offset: 0x00132E8D
		public string ParentFolderId { get; protected set; }

		// Token: 0x060062F8 RID: 25336 RVA: 0x00134C96 File Offset: 0x00132E96
		public CreateContactRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x00134C9F File Offset: 0x00132E9F
		public override ODataCommand GetODataCommand()
		{
			return new CreateContactCommand(this);
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x00134CA8 File Offset: 0x00132EA8
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment)
			{
				if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment && base.ODataContext.ODataPath.ParentOfEntitySegment.EdmType.Equals(Contact.EdmEntityType))
				{
					this.ParentFolderId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
					return;
				}
				if (string.Equals(base.ODataContext.ODataPath.EntitySegment.GetPropertyName(), UserSchema.Contacts.Name))
				{
					this.ParentFolderId = 1.ToString();
				}
			}
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x00134D60 File Offset: 0x00132F60
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.ParentFolderId);
		}
	}
}
