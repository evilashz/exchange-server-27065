using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EE6 RID: 3814
	[AllowedOAuthGrant("Mail.Read")]
	[AllowedOAuthGrant("Mail.Write")]
	internal class FindMessagesRequest : FindEntitiesRequest<Message>
	{
		// Token: 0x0600627D RID: 25213 RVA: 0x00134074 File Offset: 0x00132274
		public FindMessagesRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x0600627E RID: 25214 RVA: 0x0013407D File Offset: 0x0013227D
		// (set) Token: 0x0600627F RID: 25215 RVA: 0x00134085 File Offset: 0x00132285
		public string ParentFolderId { get; protected set; }

		// Token: 0x06006280 RID: 25216 RVA: 0x00134090 File Offset: 0x00132290
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

		// Token: 0x06006281 RID: 25217 RVA: 0x0013411F File Offset: 0x0013231F
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.ParentFolderId);
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00134132 File Offset: 0x00132332
		public override ODataCommand GetODataCommand()
		{
			return new FindMessagesCommand(this);
		}
	}
}
