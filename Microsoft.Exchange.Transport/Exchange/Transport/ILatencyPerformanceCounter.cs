using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000177 RID: 375
	internal interface ILatencyPerformanceCounter
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600106E RID: 4206
		LatencyPerformanceCounterType CounterType { get; }

		// Token: 0x0600106F RID: 4207
		void AddValue(long latencySeconds);

		// Token: 0x06001070 RID: 4208
		void AddValue(long latencySeconds, DeliveryPriority priority);

		// Token: 0x06001071 RID: 4209
		void Update();

		// Token: 0x06001072 RID: 4210
		void Reset();
	}
}
