using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000423 RID: 1059
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SetDagNetworkConfigRequest
	{
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000C37F1 File Offset: 0x000C19F1
		// (set) Token: 0x06002F83 RID: 12163 RVA: 0x000C37F9 File Offset: 0x000C19F9
		public ushort ReplicationPort
		{
			get
			{
				return this.m_replicationPort;
			}
			set
			{
				this.m_replicationPort = value;
				this.m_setPort = true;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x000C3809 File Offset: 0x000C1A09
		public bool SetPort
		{
			get
			{
				return this.m_setPort;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000C3811 File Offset: 0x000C1A11
		// (set) Token: 0x06002F86 RID: 12166 RVA: 0x000C3819 File Offset: 0x000C1A19
		public bool WhatIf
		{
			get
			{
				return this.m_whatIf;
			}
			set
			{
				this.m_whatIf = value;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000C3822 File Offset: 0x000C1A22
		// (set) Token: 0x06002F88 RID: 12168 RVA: 0x000C382A File Offset: 0x000C1A2A
		public DatabaseAvailabilityGroup.NetworkOption NetworkCompression
		{
			get
			{
				return this.m_networkCompression;
			}
			set
			{
				this.m_networkCompression = value;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000C3833 File Offset: 0x000C1A33
		// (set) Token: 0x06002F8A RID: 12170 RVA: 0x000C383B File Offset: 0x000C1A3B
		public DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
		{
			get
			{
				return this.m_networkEncryption;
			}
			set
			{
				this.m_networkEncryption = value;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000C3844 File Offset: 0x000C1A44
		// (set) Token: 0x06002F8C RID: 12172 RVA: 0x000C384C File Offset: 0x000C1A4C
		public bool DiscoverNetworks
		{
			get
			{
				return this.m_discoverNetworks;
			}
			set
			{
				this.m_discoverNetworks = value;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000C3855 File Offset: 0x000C1A55
		// (set) Token: 0x06002F8E RID: 12174 RVA: 0x000C385D File Offset: 0x000C1A5D
		public bool ManualDagNetworkConfiguration
		{
			get
			{
				return this.m_manualDagNetworkConfiguration;
			}
			set
			{
				this.m_manualDagNetworkConfiguration = value;
			}
		}

		// Token: 0x040019F4 RID: 6644
		private ushort m_replicationPort = 64327;

		// Token: 0x040019F5 RID: 6645
		private bool m_setPort;

		// Token: 0x040019F6 RID: 6646
		private bool m_whatIf;

		// Token: 0x040019F7 RID: 6647
		private DatabaseAvailabilityGroup.NetworkOption m_networkCompression = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x040019F8 RID: 6648
		private DatabaseAvailabilityGroup.NetworkOption m_networkEncryption = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x040019F9 RID: 6649
		private bool m_discoverNetworks;

		// Token: 0x040019FA RID: 6650
		private bool m_manualDagNetworkConfiguration;
	}
}
