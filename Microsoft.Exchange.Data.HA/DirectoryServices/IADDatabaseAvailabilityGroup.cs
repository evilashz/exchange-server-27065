using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADDatabaseAvailabilityGroup : IADObjectCommon
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000095 RID: 149
		DatacenterActivationModeOption DatacenterActivationMode { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000096 RID: 150
		ThirdPartyReplicationMode ThirdPartyReplication { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000097 RID: 151
		MultiValuedProperty<ADObjectId> Servers { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000098 RID: 152
		NonRootLocalLongFullPath AutoDagVolumesRootFolderPath { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000099 RID: 153
		NonRootLocalLongFullPath AutoDagDatabasesRootFolderPath { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600009A RID: 154
		int AutoDagDatabaseCopiesPerVolume { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600009B RID: 155
		int AutoDagDatabaseCopiesPerDatabase { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600009C RID: 156
		int AutoDagTotalNumberOfDatabases { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600009D RID: 157
		int AutoDagTotalNumberOfServers { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600009E RID: 158
		bool ReplayLagManagerEnabled { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600009F RID: 159
		bool AutoDagAutoReseedEnabled { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000A0 RID: 160
		bool AutoDagDiskReclaimerEnabled { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000A1 RID: 161
		bool AutoDagBitlockerEnabled { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000A2 RID: 162
		bool AutoDagFIPSCompliant { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000A3 RID: 163
		ushort ReplicationPort { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000A4 RID: 164
		MultiValuedProperty<string> StoppedMailboxServers { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000A5 RID: 165
		MultiValuedProperty<string> StartedMailboxServers { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000A6 RID: 166
		bool AllowCrossSiteRpcClientAccess { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000A7 RID: 167
		bool ManualDagNetworkConfiguration { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000A8 RID: 168
		DatabaseAvailabilityGroup.NetworkOption NetworkCompression { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000A9 RID: 169
		DatabaseAvailabilityGroup.NetworkOption NetworkEncryption { get; }
	}
}
