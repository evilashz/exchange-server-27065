using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002F9 RID: 761
	internal class PlatformDiversionInfo
	{
		// Token: 0x0600173C RID: 5948 RVA: 0x0006371C File Offset: 0x0006191C
		public PlatformDiversionInfo(string header, string calledParty, string userAtHost, RedirectReason reason, DiversionSource source)
		{
			this.DiversionHeader = header;
			this.OriginalCalledParty = calledParty;
			this.UserAtHost = userAtHost;
			this.RedirectReason = reason;
			this.DiversionSource = source;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00063749 File Offset: 0x00061949
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00063751 File Offset: 0x00061951
		public string DiversionHeader { get; private set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0006375A File Offset: 0x0006195A
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x00063762 File Offset: 0x00061962
		public string OriginalCalledParty { get; private set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0006376B File Offset: 0x0006196B
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x00063773 File Offset: 0x00061973
		public string UserAtHost { get; private set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0006377C File Offset: 0x0006197C
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x00063784 File Offset: 0x00061984
		public RedirectReason RedirectReason { get; private set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0006378D File Offset: 0x0006198D
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x00063795 File Offset: 0x00061995
		public DiversionSource DiversionSource { get; private set; }
	}
}
