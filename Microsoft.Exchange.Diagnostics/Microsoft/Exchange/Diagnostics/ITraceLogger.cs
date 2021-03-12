using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A0 RID: 160
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ITraceLogger
	{
		// Token: 0x060003C8 RID: 968
		void LogTraces(ITracer tracer);
	}
}
