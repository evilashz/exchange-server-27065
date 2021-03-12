using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B2A RID: 2858
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MdbAvailabilityResourceHealthMonitorKey : MdbResourceKey
	{
		// Token: 0x06006784 RID: 26500 RVA: 0x001B57AD File Offset: 0x001B39AD
		public MdbAvailabilityResourceHealthMonitorKey(Guid databaseGuid) : base(ResourceMetricType.MdbAvailability, databaseGuid)
		{
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x001B57B7 File Offset: 0x001B39B7
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new MdbAvailabilityResourceHealthMonitor(this);
		}
	}
}
