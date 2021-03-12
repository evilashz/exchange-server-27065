using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000488 RID: 1160
	public class SubscribeToMessageChangesRequest : BaseRequest
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000A31BF File Offset: 0x000A13BF
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x000A31C7 File Offset: 0x000A13C7
		public string SubscriptionId { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x000A31D0 File Offset: 0x000A13D0
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x000A31D8 File Offset: 0x000A13D8
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x000A31E1 File Offset: 0x000A13E1
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x000A31E9 File Offset: 0x000A13E9
		public ItemResponseShape MessageShape { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000A31F2 File Offset: 0x000A13F2
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x000A31FA File Offset: 0x000A13FA
		public SortResults[] SortOrder { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000A3203 File Offset: 0x000A1403
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x000A320B File Offset: 0x000A140B
		internal Guid[] MailboxGuids { get; set; }

		// Token: 0x0600227A RID: 8826 RVA: 0x000A3214 File Offset: 0x000A1414
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000A321B File Offset: 0x000A141B
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			throw new NotImplementedException();
		}
	}
}
