using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED4 RID: 3796
	[AllowedOAuthGrant("Mail.Write")]
	internal class CreateFolderRequest : CreateEntityRequest<Folder>
	{
		// Token: 0x0600625B RID: 25179 RVA: 0x00133D30 File Offset: 0x00131F30
		public CreateFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x0600625C RID: 25180 RVA: 0x00133D39 File Offset: 0x00131F39
		// (set) Token: 0x0600625D RID: 25181 RVA: 0x00133D41 File Offset: 0x00131F41
		public string ParentFolderId { get; protected set; }

		// Token: 0x0600625E RID: 25182 RVA: 0x00133D4C File Offset: 0x00131F4C
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment)
			{
				if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment)
				{
					this.ParentFolderId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
					return;
				}
				if (base.ODataContext.ODataPath.ParentOfEntitySegment is NavigationPropertySegment)
				{
					this.ParentFolderId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetPropertyName();
				}
			}
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x00133DDB File Offset: 0x00131FDB
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.ParentFolderId);
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x00133DEE File Offset: 0x00131FEE
		public override ODataCommand GetODataCommand()
		{
			return new CreateFolderCommand(this);
		}
	}
}
