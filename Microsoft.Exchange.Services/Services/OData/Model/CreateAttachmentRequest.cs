using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F0B RID: 3851
	internal abstract class CreateAttachmentRequest : CreateEntityRequest<Attachment>
	{
		// Token: 0x060062C9 RID: 25289 RVA: 0x00134880 File Offset: 0x00132A80
		public CreateAttachmentRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x00134889 File Offset: 0x00132A89
		// (set) Token: 0x060062CB RID: 25291 RVA: 0x00134891 File Offset: 0x00132A91
		public string RootItemId { get; protected set; }

		// Token: 0x060062CC RID: 25292 RVA: 0x0013489C File Offset: 0x00132A9C
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment && base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment)
			{
				this.RootItemId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
			}
			base.Template.ParentItemNavigationName = this.ParentItemNavigationName;
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x060062CD RID: 25293
		protected abstract string ParentItemNavigationName { get; }

		// Token: 0x060062CE RID: 25294 RVA: 0x00134909 File Offset: 0x00132B09
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.RootItemId);
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x0013491C File Offset: 0x00132B1C
		public override ODataCommand GetODataCommand()
		{
			return new CreateAttachmentCommand(this);
		}
	}
}
