using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F05 RID: 3845
	internal abstract class DeleteAttachmentRequest : DeleteEntityRequest<Attachment>
	{
		// Token: 0x060062BD RID: 25277 RVA: 0x001347A2 File Offset: 0x001329A2
		public DeleteAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x060062BE RID: 25278 RVA: 0x001347AB File Offset: 0x001329AB
		// (set) Token: 0x060062BF RID: 25279 RVA: 0x001347B3 File Offset: 0x001329B3
		public string RootItemId { get; protected set; }

		// Token: 0x060062C0 RID: 25280 RVA: 0x001347BC File Offset: 0x001329BC
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.GrandParentOfEntitySegment is KeySegment)
			{
				this.RootItemId = base.ODataContext.ODataPath.GrandParentOfEntitySegment.GetIdKey();
			}
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x001347F6 File Offset: 0x001329F6
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.RootItemId);
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x00134809 File Offset: 0x00132A09
		public override ODataCommand GetODataCommand()
		{
			return new DeleteAttachmentCommand(this);
		}
	}
}
