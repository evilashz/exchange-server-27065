using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F2 RID: 242
	internal interface IScopedPerformanceMonitor
	{
		// Token: 0x060006D4 RID: 1748
		bool Start(ScopeInfo scopeInfo);

		// Token: 0x060006D5 RID: 1749
		void End(ScopeInfo scopeInfo);
	}
}
