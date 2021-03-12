using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B2D RID: 2861
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MdbReplicationResourceHealthMonitorKey : MdbResourceKey
	{
		// Token: 0x0600678C RID: 26508 RVA: 0x001B5861 File Offset: 0x001B3A61
		public MdbReplicationResourceHealthMonitorKey(Guid databaseGuid) : base(ResourceMetricType.MdbReplication, databaseGuid)
		{
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x001B586B File Offset: 0x001B3A6B
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new MdbReplicationResourceHealthMonitor(this);
		}
	}
}
