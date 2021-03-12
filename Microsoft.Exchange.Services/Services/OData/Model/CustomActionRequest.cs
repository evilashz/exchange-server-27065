using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB7 RID: 3767
	internal abstract class CustomActionRequest : ODataRequest
	{
		// Token: 0x0600620B RID: 25099 RVA: 0x00133519 File Offset: 0x00131719
		public CustomActionRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x00133522 File Offset: 0x00131722
		// (set) Token: 0x0600620D RID: 25101 RVA: 0x0013352A File Offset: 0x0013172A
		protected string ActionName { get; set; }

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x0600620E RID: 25102 RVA: 0x00133533 File Offset: 0x00131733
		// (set) Token: 0x0600620F RID: 25103 RVA: 0x0013353B File Offset: 0x0013173B
		protected IDictionary<string, object> Parameters { get; set; }

		// Token: 0x06006210 RID: 25104 RVA: 0x00133544 File Offset: 0x00131744
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			this.ActionName = base.ODataContext.ODataPath.EntitySegment.GetActionName();
			this.Parameters = base.ReadPostBodyAsParameters();
		}
	}
}
