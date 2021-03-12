using System;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x0200010E RID: 270
	public class AmDbStatusInfo2
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x00002D24 File Offset: 0x00002124
		public AmDbStatusInfo2(AmDbStatusInfo2 info)
		{
			this.m_masterServerFqdn = info.m_masterServerFqdn;
			this.m_isHighlyAvailable = info.m_isHighlyAvailable;
			this.m_lastMountedServerFqdn = info.m_lastMountedServerFqdn;
			DateTime mountedTime = info.m_mountedTime;
			this.m_mountedTime = mountedTime;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00001C0C File Offset: 0x0000100C
		public AmDbStatusInfo2(string masterServerFqdn, int isHighlyAvailable, string lastMountedServerFqdn, DateTime mountedTime)
		{
			this.m_masterServerFqdn = masterServerFqdn;
			this.m_isHighlyAvailable = isHighlyAvailable;
			this.m_lastMountedServerFqdn = lastMountedServerFqdn;
			this.m_mountedTime = mountedTime;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00001C3C File Offset: 0x0000103C
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00001C50 File Offset: 0x00001050
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00001C64 File Offset: 0x00001064
		public int IsHighlyAvailable
		{
			get
			{
				return this.m_isHighlyAvailable;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00001C78 File Offset: 0x00001078
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00001C8C File Offset: 0x0000108C
		public string LastMountedServerFqdn
		{
			get
			{
				return this.m_lastMountedServerFqdn;
			}
			set
			{
				this.m_lastMountedServerFqdn = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00001CA0 File Offset: 0x000010A0
		public DateTime MountedTime
		{
			get
			{
				return this.m_mountedTime;
			}
		}

		// Token: 0x04000943 RID: 2371
		private string m_masterServerFqdn;

		// Token: 0x04000944 RID: 2372
		private int m_isHighlyAvailable;

		// Token: 0x04000945 RID: 2373
		private string m_lastMountedServerFqdn;

		// Token: 0x04000946 RID: 2374
		private DateTime m_mountedTime;
	}
}
