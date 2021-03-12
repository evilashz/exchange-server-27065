using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F2C RID: 3884
	[AllowedOAuthGrant("Contacts.Write")]
	[AllowedOAuthGrant("Contacts.Read")]
	internal class FindContactFoldersRequest : FindEntitiesRequest<ContactFolder>
	{
		// Token: 0x0600631C RID: 25372 RVA: 0x00135122 File Offset: 0x00133322
		public FindContactFoldersRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x0600631D RID: 25373 RVA: 0x0013512B File Offset: 0x0013332B
		// (set) Token: 0x0600631E RID: 25374 RVA: 0x00135133 File Offset: 0x00133333
		public string ParentFolderId { get; protected set; }

		// Token: 0x0600631F RID: 25375 RVA: 0x0013513C File Offset: 0x0013333C
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

		// Token: 0x06006320 RID: 25376 RVA: 0x001351BC File Offset: 0x001333BC
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.ParentFolderId);
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x001351CF File Offset: 0x001333CF
		public override ODataCommand GetODataCommand()
		{
			return new FindContactFoldersCommand(this);
		}
	}
}
