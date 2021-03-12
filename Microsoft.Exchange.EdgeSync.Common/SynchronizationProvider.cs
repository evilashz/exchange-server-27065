using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000006 RID: 6
	internal abstract class SynchronizationProvider
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40
		public abstract string Identity { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41
		public abstract List<TargetServerConfig> TargetServerConfigs { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002A RID: 42
		public abstract int LeaseLockTryCount { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43
		public abstract EnhancedTimeSpan RecipientSyncInterval { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44
		public abstract EnhancedTimeSpan ConfigurationSyncInterval { get; }

		// Token: 0x0600002D RID: 45
		public abstract void Initialize(EdgeSyncConnector connector);

		// Token: 0x0600002E RID: 46
		public abstract List<TypeSynchronizer> CreateTypeSynchronizer(SyncTreeType type);

		// Token: 0x0600002F RID: 47
		public abstract TargetConnection CreateTargetConnection(TargetServerConfig targetServerConfig, SyncTreeType type, TestShutdownAndLeaseDelegate testShutdownAndLease, EdgeSyncLogSession logSession);
	}
}
