using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000107 RID: 263
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReplayCoreManager : IServiceComponent
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A51 RID: 2641
		ReplicaInstanceManager ReplicaInstanceManager { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A52 RID: 2642
		ReplaySystemQueue SystemQueue { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A53 RID: 2643
		IRunConfigurationUpdater ConfigurationUpdater { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A54 RID: 2644
		DumpsterRedeliveryManager DumpsterRedeliveryManager { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A55 RID: 2645
		SkippedLogsDeleter SkippedLogsDeleter { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A56 RID: 2646
		AmSearchServiceMonitor SearchServiceMonitor { get; }
	}
}
