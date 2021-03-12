using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F8 RID: 2552
	internal interface IRPCLatencyProvider : IResourceLoadMonitor
	{
		// Token: 0x0600765F RID: 30303
		void Update(int averageRpcLatency, uint totalDbOperations);

		// Token: 0x17002A64 RID: 10852
		// (get) Token: 0x06007660 RID: 30304
		int LastRPCLatencyValue { get; }

		// Token: 0x17002A65 RID: 10853
		// (get) Token: 0x06007661 RID: 30305
		int AverageRPCLatencyValue { get; }
	}
}
