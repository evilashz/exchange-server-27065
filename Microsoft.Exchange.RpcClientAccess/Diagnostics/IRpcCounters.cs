using System;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200002B RID: 43
	internal interface IRpcCounters
	{
		// Token: 0x06000187 RID: 391
		void IncrementCounter(IRpcCounterData counterData);
	}
}
