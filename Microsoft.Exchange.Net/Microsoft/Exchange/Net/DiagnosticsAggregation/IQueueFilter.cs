using System;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000844 RID: 2116
	internal interface IQueueFilter
	{
		// Token: 0x06002D20 RID: 11552
		bool Match(LocalQueueInfo localQueue);
	}
}
