using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EFF RID: 3839
	internal abstract class GetAttachmentRequest : GetEntityRequest<Attachment>
	{
		// Token: 0x060062B1 RID: 25265 RVA: 0x0013468F File Offset: 0x0013288F
		public GetAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x060062B2 RID: 25266 RVA: 0x00134698 File Offset: 0x00132898
		// (set) Token: 0x060062B3 RID: 25267 RVA: 0x001346A0 File Offset: 0x001328A0
		public string RootItemId { get; protected set; }

		// Token: 0x060062B4 RID: 25268 RVA: 0x001346A9 File Offset: 0x001328A9
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.GrandParentOfEntitySegment is KeySegment)
			{
				this.RootItemId = base.ODataContext.ODataPath.GrandParentOfEntitySegment.GetIdKey();
			}
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x001346E3 File Offset: 0x001328E3
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.RootItemId);
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x001346F6 File Offset: 0x001328F6
		public override ODataCommand GetODataCommand()
		{
			return new GetAttachmentCommand(this);
		}
	}
}
