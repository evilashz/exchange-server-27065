using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncRPCResourceMonitor : SyncResourceMonitor
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000CBC1 File Offset: 0x0000ADC1
		public SyncRPCResourceMonitor(SyncLogSession syncLogSession, ResourceKey resourceKey, SyncResourceMonitorType syncResourceMonitorType) : base(syncLogSession, resourceKey, syncResourceMonitorType)
		{
			this.rpcResourceHealthMonitor = (IRPCLatencyProvider)base.ResourceHealthMonitor;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CBDD File Offset: 0x0000ADDD
		public virtual float GetRawRpcLatency()
		{
			return (float)this.rpcResourceHealthMonitor.LastRPCLatencyValue;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		public virtual float GetRawRpcLatencyAverage()
		{
			return (float)this.rpcResourceHealthMonitor.AverageRPCLatencyValue;
		}

		// Token: 0x04000184 RID: 388
		private IRPCLatencyProvider rpcResourceHealthMonitor;
	}
}
