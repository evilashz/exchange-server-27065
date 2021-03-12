using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000487 RID: 1159
	public class SubscribeToHierarchyChangesRequest : BaseRequest
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x000A3187 File Offset: 0x000A1387
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x000A318F File Offset: 0x000A138F
		public string SubscriptionId { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000A3198 File Offset: 0x000A1398
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x000A31A0 File Offset: 0x000A13A0
		public Guid MailboxGuid { get; set; }

		// Token: 0x0600226D RID: 8813 RVA: 0x000A31A9 File Offset: 0x000A13A9
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000A31B0 File Offset: 0x000A13B0
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			throw new NotImplementedException();
		}
	}
}
