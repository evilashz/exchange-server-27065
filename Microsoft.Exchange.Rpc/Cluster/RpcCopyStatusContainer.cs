using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	internal sealed class RpcCopyStatusContainer
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00005BF0 File Offset: 0x00004FF0
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00005C04 File Offset: 0x00005004
		public RpcDatabaseCopyStatus2[] CopyStatuses
		{
			get
			{
				return this.m_copyStatuses;
			}
			set
			{
				this.m_copyStatuses = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00005C18 File Offset: 0x00005018
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00005C2C File Offset: 0x0000502C
		public RpcHealthStateInfo[] HealthStates
		{
			get
			{
				return this.m_healthStates;
			}
			set
			{
				this.m_healthStates = value;
			}
		}

		// Token: 0x040009B3 RID: 2483
		private RpcDatabaseCopyStatus2[] m_copyStatuses;

		// Token: 0x040009B4 RID: 2484
		private RpcHealthStateInfo[] m_healthStates;
	}
}
