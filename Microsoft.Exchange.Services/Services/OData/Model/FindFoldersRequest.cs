using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED1 RID: 3793
	[AllowedOAuthGrant("Mail.Read")]
	[AllowedOAuthGrant("Mail.Write")]
	internal class FindFoldersRequest : FindEntitiesRequest<Folder>
	{
		// Token: 0x06006252 RID: 25170 RVA: 0x00133B2E File Offset: 0x00131D2E
		public FindFoldersRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x00133B37 File Offset: 0x00131D37
		// (set) Token: 0x06006254 RID: 25172 RVA: 0x00133B3F File Offset: 0x00131D3F
		public string ParentFolderId { get; protected set; }

		// Token: 0x06006255 RID: 25173 RVA: 0x00133B48 File Offset: 0x00131D48
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			ODataPathSegment parentOfEntitySegment = base.ODataContext.ODataPath.ParentOfEntitySegment;
			if (parentOfEntitySegment is KeySegment && parentOfEntitySegment.EdmType.Equals(Folder.EdmEntityType))
			{
				this.ParentFolderId = parentOfEntitySegment.GetIdKey();
				return;
			}
			if (parentOfEntitySegment is NavigationPropertySegment && parentOfEntitySegment.EdmType.Equals(Folder.EdmEntityType))
			{
				this.ParentFolderId = parentOfEntitySegment.GetPropertyName();
				return;
			}
			this.ParentFolderId = DistinguishedFolderIdName.msgfolderroot.ToString();
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x00133BCC File Offset: 0x00131DCC
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.ParentFolderId);
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x00133BDF File Offset: 0x00131DDF
		public override ODataCommand GetODataCommand()
		{
			return new FindFoldersCommand(this);
		}
	}
}
