using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000010 RID: 16
	internal interface IResourceTracker
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000051 RID: 81
		// (remove) Token: 0x06000052 RID: 82
		event ResourceUseChangedHandler ResourceUseChanged;

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000053 RID: 83
		ResourceUse AggregateResourceUse { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000054 RID: 84
		bool IsTracking { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000055 RID: 85
		IEnumerable<ResourceUse> ResourceUses { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000056 RID: 86
		DateTime LastUpdateTime { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000057 RID: 87
		TimeSpan TrackingInterval { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000058 RID: 88
		IEnumerable<IResourceMeter> ResourceMeters { get; }

		// Token: 0x06000059 RID: 89
		Task StartResourceTrackingAsync(CancellationToken cancellationToken);

		// Token: 0x0600005A RID: 90
		ResourceTrackerDiagnosticsData GetDiagnosticsData();
	}
}
