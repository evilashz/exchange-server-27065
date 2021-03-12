using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D2 RID: 466
	public class RemoteSubscriptionEventArgs : EventArgs
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x0003F626 File Offset: 0x0003D826
		public RemoteSubscriptionEventArgs(string contextKey, string subscriptionId)
		{
			this.ContextKey = contextKey;
			this.SubscriptionId = subscriptionId;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0003F63C File Offset: 0x0003D83C
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x0003F644 File Offset: 0x0003D844
		public string ContextKey { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0003F64D File Offset: 0x0003D84D
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x0003F655 File Offset: 0x0003D855
		public string SubscriptionId { get; private set; }
	}
}
