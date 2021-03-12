using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x0200023F RID: 575
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IVolumeManager
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060015E2 RID: 5602
		IEnumerable<ExchangeVolume> Volumes { get; }

		// Token: 0x060015E3 RID: 5603
		ExchangeVolume AssignSpare(DatabaseSpareInfo[] dbInfos);

		// Token: 0x060015E4 RID: 5604
		ExchangeVolume FixupMountPointForDatabase(DatabaseSpareInfo dbInfo, MountedFolderPath volumeToAssign);

		// Token: 0x060015E5 RID: 5605
		bool FixActiveDatabaseMountPoint(IADDatabase database, IEnumerable<IADDatabase> databases, IMonitoringADConfig adConfig, out Exception exception, bool checkDatabaseGroupExists = true);

		// Token: 0x060015E6 RID: 5606
		void UpdateVolumeInfoCopyState(Guid guid, IReplicaInstanceManager rim);
	}
}
