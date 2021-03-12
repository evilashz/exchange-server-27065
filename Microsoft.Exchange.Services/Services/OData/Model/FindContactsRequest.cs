using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F20 RID: 3872
	[AllowedOAuthGrant("Contacts.Write")]
	[AllowedOAuthGrant("Contacts.Read")]
	internal class FindContactsRequest : FindEntitiesRequest<Contact>
	{
		// Token: 0x06006304 RID: 25348 RVA: 0x00134E4C File Offset: 0x0013304C
		public FindContactsRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x00134E55 File Offset: 0x00133055
		// (set) Token: 0x06006306 RID: 25350 RVA: 0x00134E5D File Offset: 0x0013305D
		public string ParentFolderId { get; protected set; }

		// Token: 0x06006307 RID: 25351 RVA: 0x00134E66 File Offset: 0x00133066
		public override ODataCommand GetODataCommand()
		{
			return new FindContactsCommand(this);
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x00134E70 File Offset: 0x00133070
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			ODataPathSegment parentOfEntitySegment = base.ODataContext.ODataPath.ParentOfEntitySegment;
			if (parentOfEntitySegment.EdmType.Equals(User.EdmEntityType))
			{
				this.ParentFolderId = DistinguishedFolderIdName.contacts.ToString();
				return;
			}
			if (parentOfEntitySegment is KeySegment && parentOfEntitySegment.EdmType.Equals(ContactFolder.EdmEntityType))
			{
				this.ParentFolderId = parentOfEntitySegment.GetIdKey();
				return;
			}
			this.ParentFolderId = DistinguishedFolderIdName.contacts.ToString();
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00134EF0 File Offset: 0x001330F0
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.ParentFolderId);
		}
	}
}
