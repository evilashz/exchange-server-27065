using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200009D RID: 157
	public interface ITraceable
	{
		// Token: 0x060003BD RID: 957
		void TraceTo(ITraceBuilder traceBuilder);
	}
}
