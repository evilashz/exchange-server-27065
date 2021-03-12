using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	public class PersistentDagNetwork
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x000630D8 File Offset: 0x000612D8
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x000630E0 File Offset: 0x000612E0
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x000630E9 File Offset: 0x000612E9
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x000630F1 File Offset: 0x000612F1
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000630FA File Offset: 0x000612FA
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x00063102 File Offset: 0x00061302
		public bool ReplicationEnabled
		{
			get
			{
				return this.m_replicationEnabled;
			}
			set
			{
				this.m_replicationEnabled = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0006310B File Offset: 0x0006130B
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00063113 File Offset: 0x00061313
		public bool IgnoreNetwork
		{
			get
			{
				return this.m_ignoreNetwork;
			}
			set
			{
				this.m_ignoreNetwork = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0006311C File Offset: 0x0006131C
		public List<string> Subnets
		{
			get
			{
				return this.m_subnets;
			}
		}

		// Token: 0x04000976 RID: 2422
		private string m_name;

		// Token: 0x04000977 RID: 2423
		private string m_description;

		// Token: 0x04000978 RID: 2424
		private bool m_replicationEnabled;

		// Token: 0x04000979 RID: 2425
		private bool m_ignoreNetwork;

		// Token: 0x0400097A RID: 2426
		private List<string> m_subnets = new List<string>();
	}
}
