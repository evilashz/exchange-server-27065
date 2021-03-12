using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200002E RID: 46
	internal interface IResourceAdmissionControl
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000183 RID: 387
		ResourceKey ResourceKey { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000184 RID: 388
		bool IsAcquired { get; }

		// Token: 0x06000185 RID: 389
		bool TryAcquire(WorkloadClassification classification, out double delayRatio);

		// Token: 0x06000186 RID: 390
		void Release(WorkloadClassification classification);
	}
}
