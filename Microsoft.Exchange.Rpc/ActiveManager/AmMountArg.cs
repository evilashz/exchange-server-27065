using System;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x0200010F RID: 271
	public class AmMountArg
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x00001CE8 File Offset: 0x000010E8
		public AmMountArg(AmMountArg amMountArg)
		{
			this.m_lastMountedServer = amMountArg.m_lastMountedServer;
			this.m_storeFlags = amMountArg.m_storeFlags;
			this.m_amMountFlags = amMountArg.m_amMountFlags;
			this.m_reason = amMountArg.m_reason;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00001CB8 File Offset: 0x000010B8
		public AmMountArg(int storeFlags, int amFlags, string lastMountedServer, int reason)
		{
			this.m_lastMountedServer = lastMountedServer;
			this.m_storeFlags = storeFlags;
			this.m_amMountFlags = amFlags;
			this.m_reason = reason;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00001D2C File Offset: 0x0000112C
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00001D40 File Offset: 0x00001140
		public string LastMountedServer
		{
			get
			{
				return this.m_lastMountedServer;
			}
			set
			{
				this.m_lastMountedServer = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00001D54 File Offset: 0x00001154
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00001D68 File Offset: 0x00001168
		public int StoreFlags
		{
			get
			{
				return this.m_storeFlags;
			}
			set
			{
				this.m_storeFlags = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00001D7C File Offset: 0x0000117C
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00001D90 File Offset: 0x00001190
		public int AmMountFlags
		{
			get
			{
				return this.m_amMountFlags;
			}
			set
			{
				this.m_amMountFlags = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00001DA4 File Offset: 0x000011A4
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00001DB8 File Offset: 0x000011B8
		public int Reason
		{
			get
			{
				return this.m_reason;
			}
			set
			{
				this.m_reason = value;
			}
		}

		// Token: 0x04000947 RID: 2375
		private int m_storeFlags;

		// Token: 0x04000948 RID: 2376
		private int m_amMountFlags;

		// Token: 0x04000949 RID: 2377
		private int m_reason;

		// Token: 0x0400094A RID: 2378
		private string m_lastMountedServer;
	}
}
