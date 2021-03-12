using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B30 RID: 2864
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MdbResourceHealthMonitorKey : MdbResourceKey
	{
		// Token: 0x060067A2 RID: 26530 RVA: 0x001B5C8D File Offset: 0x001B3E8D
		public MdbResourceHealthMonitorKey(Guid databaseGuid) : base(ResourceMetricType.MdbLatency, databaseGuid)
		{
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x001B5C97 File Offset: 0x001B3E97
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new MdbResourceHealthMonitor(this);
		}
	}
}
