using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EF9 RID: 3833
	internal abstract class FindAttachmentsRequest : FindEntitiesRequest<Attachment>
	{
		// Token: 0x060062A5 RID: 25253 RVA: 0x00134565 File Offset: 0x00132765
		public FindAttachmentsRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060062A6 RID: 25254 RVA: 0x0013456E File Offset: 0x0013276E
		// (set) Token: 0x060062A7 RID: 25255 RVA: 0x00134576 File Offset: 0x00132776
		public string RootItemId { get; protected set; }

		// Token: 0x060062A8 RID: 25256 RVA: 0x00134580 File Offset: 0x00132780
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment && base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment)
			{
				this.RootItemId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
			}
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x001345DC File Offset: 0x001327DC
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.RootItemId);
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x001345EF File Offset: 0x001327EF
		public override ODataCommand GetODataCommand()
		{
			return new FindAttachmentsCommand(this);
		}
	}
}
