using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009FB RID: 2555
	internal class DummyResourceHealthMonitorWrapper : ResourceHealthMonitorWrapper, IRPCLatencyProvider, IDatabaseReplicationProvider, IDatabaseAvailabilityProvider, IResourceLoadMonitor
	{
		// Token: 0x06007664 RID: 30308 RVA: 0x00185A27 File Offset: 0x00183C27
		public DummyResourceHealthMonitorWrapper(DummyResourceHealthMonitor monitor) : base(monitor)
		{
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x00185A30 File Offset: 0x00183C30
		void IRPCLatencyProvider.Update(int averageRpcLatency, uint totalDbOperations)
		{
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x00185A32 File Offset: 0x00183C32
		void IDatabaseReplicationProvider.Update(uint databaseReplicationHealth)
		{
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x00185A34 File Offset: 0x00183C34
		void IDatabaseAvailabilityProvider.Update(uint databaseAvailabilityHealth)
		{
		}

		// Token: 0x17002A66 RID: 10854
		// (get) Token: 0x06007668 RID: 30312 RVA: 0x00185A36 File Offset: 0x00183C36
		int IRPCLatencyProvider.LastRPCLatencyValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002A67 RID: 10855
		// (get) Token: 0x06007669 RID: 30313 RVA: 0x00185A39 File Offset: 0x00183C39
		int IRPCLatencyProvider.AverageRPCLatencyValue
		{
			get
			{
				return 0;
			}
		}
	}
}
