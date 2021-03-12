using System;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000110 RID: 272
	public class AmDismountArg
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x00001DF0 File Offset: 0x000011F0
		public AmDismountArg(AmDismountArg amDismountArg)
		{
			this.m_flags = amDismountArg.m_flags;
			this.m_reason = amDismountArg.m_reason;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00001DCC File Offset: 0x000011CC
		public AmDismountArg(int flags, int reason)
		{
			this.m_flags = flags;
			this.m_reason = reason;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00001E1C File Offset: 0x0000121C
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00001E30 File Offset: 0x00001230
		public int Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.m_flags = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00001E44 File Offset: 0x00001244
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00001E58 File Offset: 0x00001258
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

		// Token: 0x0400094B RID: 2379
		private int m_flags;

		// Token: 0x0400094C RID: 2380
		private int m_reason;
	}
}
