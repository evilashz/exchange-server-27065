using System;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x0200010D RID: 269
	public class AmDbStatusInfo
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00002CF8 File Offset: 0x000020F8
		public AmDbStatusInfo(AmDbStatusInfo info)
		{
			this.m_masterServerFqdn = info.m_masterServerFqdn;
			this.m_isHighlyAvailable = info.m_isHighlyAvailable;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00001BAC File Offset: 0x00000FAC
		public AmDbStatusInfo(string masterServerFqdn, int isHighlyAvailable)
		{
			this.m_masterServerFqdn = masterServerFqdn;
			this.m_isHighlyAvailable = isHighlyAvailable;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00001BD0 File Offset: 0x00000FD0
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00001BE4 File Offset: 0x00000FE4
		public string MasterServerFqdn
		{
			get
			{
				return this.m_masterServerFqdn;
			}
			set
			{
				this.m_masterServerFqdn = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00001BF8 File Offset: 0x00000FF8
		public int IsHighlyAvailable
		{
			get
			{
				return this.m_isHighlyAvailable;
			}
		}

		// Token: 0x04000941 RID: 2369
		private string m_masterServerFqdn;

		// Token: 0x04000942 RID: 2370
		private int m_isHighlyAvailable;
	}
}
