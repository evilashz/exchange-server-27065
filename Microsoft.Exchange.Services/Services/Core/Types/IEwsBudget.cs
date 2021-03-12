using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003DB RID: 987
	internal interface IEwsBudget : IStandardBudget, IBudget, IDisposable
	{
		// Token: 0x06001BA9 RID: 7081
		bool SleepIfNecessary();

		// Token: 0x06001BAA RID: 7082
		bool SleepIfNecessary(out int sleepTime, out float cpuPercent);

		// Token: 0x06001BAB RID: 7083
		void LogEndStateToIIS();

		// Token: 0x06001BAC RID: 7084
		bool TryIncrementFoundObjectCount(uint foundCount, out int maxPossible);

		// Token: 0x06001BAD RID: 7085
		bool CanAllocateFoundObjects(uint foundCount, out int maxPossible);

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001BAE RID: 7086
		uint TotalRpcRequestCount { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001BAF RID: 7087
		ulong TotalRpcRequestLatency { get; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001BB0 RID: 7088
		uint TotalLdapRequestCount { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001BB1 RID: 7089
		long TotalLdapRequestLatency { get; }

		// Token: 0x06001BB2 RID: 7090
		void StartPerformanceContext();

		// Token: 0x06001BB3 RID: 7091
		void StopPerformanceContext();
	}
}
