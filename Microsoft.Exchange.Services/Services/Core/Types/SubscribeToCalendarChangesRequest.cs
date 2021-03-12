using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000485 RID: 1157
	public class SubscribeToCalendarChangesRequest : BaseRequest
	{
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000A30E4 File Offset: 0x000A12E4
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x000A30EC File Offset: 0x000A12EC
		public string SubscriptionId { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000A30F5 File Offset: 0x000A12F5
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x000A30FD File Offset: 0x000A12FD
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x06002259 RID: 8793 RVA: 0x000A3106 File Offset: 0x000A1306
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000A310D File Offset: 0x000A130D
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			throw new NotImplementedException();
		}
	}
}
