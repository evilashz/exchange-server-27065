using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002D9 RID: 729
	internal interface IReplayAdObjectLookup
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001CFC RID: 7420
		ITopologyConfigurationSession AdSession { get; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001CFD RID: 7421
		IFindAdObject<IADDatabaseAvailabilityGroup> DagLookup { get; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001CFE RID: 7422
		IFindAdObject<IADDatabase> DatabaseLookup { get; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001CFF RID: 7423
		IFindAdObject<IADServer> ServerLookup { get; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001D00 RID: 7424
		IFindMiniServer MiniServerLookup { get; }

		// Token: 0x06001D01 RID: 7425
		void Clear();
	}
}
