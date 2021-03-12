using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000486 RID: 1158
	public class SubscribeToConversationChangesRequest : BaseRequest
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000A311C File Offset: 0x000A131C
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x000A3124 File Offset: 0x000A1324
		public string SubscriptionId { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000A312D File Offset: 0x000A132D
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x000A3135 File Offset: 0x000A1335
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000A313E File Offset: 0x000A133E
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x000A3146 File Offset: 0x000A1346
		public ConversationResponseShape ConversationShape { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000A314F File Offset: 0x000A134F
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x000A3157 File Offset: 0x000A1357
		public SortResults[] SortOrder { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000A3160 File Offset: 0x000A1360
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x000A3168 File Offset: 0x000A1368
		internal Guid[] MailboxGuids { get; set; }

		// Token: 0x06002266 RID: 8806 RVA: 0x000A3171 File Offset: 0x000A1371
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000A3178 File Offset: 0x000A1378
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			throw new NotImplementedException();
		}
	}
}
