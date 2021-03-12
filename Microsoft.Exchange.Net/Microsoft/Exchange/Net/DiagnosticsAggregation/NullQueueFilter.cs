using System;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000845 RID: 2117
	internal class NullQueueFilter : IQueueFilter
	{
		// Token: 0x06002D21 RID: 11553 RVA: 0x000655AB File Offset: 0x000637AB
		public bool Match(LocalQueueInfo localQueue)
		{
			return true;
		}
	}
}
