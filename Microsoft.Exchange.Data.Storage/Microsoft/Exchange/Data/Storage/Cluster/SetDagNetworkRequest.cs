using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000421 RID: 1057
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SetDagNetworkRequest
	{
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000C36F7 File Offset: 0x000C18F7
		// (set) Token: 0x06002F6D RID: 12141 RVA: 0x000C36FF File Offset: 0x000C18FF
		public string Name { get; set; }

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000C3708 File Offset: 0x000C1908
		// (set) Token: 0x06002F6F RID: 12143 RVA: 0x000C3710 File Offset: 0x000C1910
		public string NewName { get; set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000C3719 File Offset: 0x000C1919
		// (set) Token: 0x06002F71 RID: 12145 RVA: 0x000C3721 File Offset: 0x000C1921
		public string Description { get; set; }

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000C372A File Offset: 0x000C192A
		public string LatestName
		{
			get
			{
				return this.NewName ?? this.Name;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000C373C File Offset: 0x000C193C
		public SortedList<DatabaseAvailabilityGroupSubnetId, object> Subnets
		{
			get
			{
				return this.m_subnets;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000C3744 File Offset: 0x000C1944
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x000C375E File Offset: 0x000C195E
		public bool SubnetListIsSet
		{
			get
			{
				return this.m_subnetsWereSet || this.m_subnets.Count > 0;
			}
			set
			{
				this.m_subnetsWereSet = value;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000C3767 File Offset: 0x000C1967
		// (set) Token: 0x06002F77 RID: 12151 RVA: 0x000C376F File Offset: 0x000C196F
		public bool IsReplicationChanged { get; set; }

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000C3778 File Offset: 0x000C1978
		// (set) Token: 0x06002F79 RID: 12153 RVA: 0x000C3780 File Offset: 0x000C1980
		public bool ReplicationEnabled
		{
			get
			{
				return this.m_replEnabled;
			}
			set
			{
				this.IsReplicationChanged = true;
				this.m_replEnabled = value;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000C3790 File Offset: 0x000C1990
		// (set) Token: 0x06002F7B RID: 12155 RVA: 0x000C3798 File Offset: 0x000C1998
		public bool IsIgnoreChanged { get; set; }

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000C37A1 File Offset: 0x000C19A1
		// (set) Token: 0x06002F7D RID: 12157 RVA: 0x000C37A9 File Offset: 0x000C19A9
		public bool IgnoreNetwork
		{
			get
			{
				return this.m_ignoreNetwork;
			}
			set
			{
				this.IsIgnoreChanged = true;
				this.m_ignoreNetwork = value;
			}
		}

		// Token: 0x040019EA RID: 6634
		private SortedList<DatabaseAvailabilityGroupSubnetId, object> m_subnets = new SortedList<DatabaseAvailabilityGroupSubnetId, object>(DagSubnetIdComparer.Comparer);

		// Token: 0x040019EB RID: 6635
		private bool m_subnetsWereSet;

		// Token: 0x040019EC RID: 6636
		private bool m_replEnabled = true;

		// Token: 0x040019ED RID: 6637
		private bool m_ignoreNetwork;
	}
}
