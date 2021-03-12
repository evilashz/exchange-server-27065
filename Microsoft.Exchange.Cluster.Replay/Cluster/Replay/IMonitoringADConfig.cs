using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000226 RID: 550
	internal interface IMonitoringADConfig
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060014D9 RID: 5337
		DateTime CreateTimeUtc { get; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060014DA RID: 5338
		List<AmServerName> AmServerNames { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060014DB RID: 5339
		IADDatabaseAvailabilityGroup Dag { get; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060014DC RID: 5340
		Dictionary<AmServerName, IEnumerable<IADDatabase>> DatabaseMap { get; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060014DD RID: 5341
		Dictionary<Guid, IADDatabase> DatabaseByGuidMap { get; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060014DE RID: 5342
		Dictionary<AmServerName, IEnumerable<IADDatabase>> DatabasesIncludingMisconfiguredMap { get; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060014DF RID: 5343
		IADServer TargetMiniServer { get; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060014E0 RID: 5344
		AmServerName TargetServerName { get; }

		// Token: 0x060014E1 RID: 5345
		IADServer LookupMiniServerByName(AmServerName serverName);

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060014E2 RID: 5346
		MonitoringServerRole ServerRole { get; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060014E3 RID: 5347
		List<IADServer> Servers { get; }
	}
}
