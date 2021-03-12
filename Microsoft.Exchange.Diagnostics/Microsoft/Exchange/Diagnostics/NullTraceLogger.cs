using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NullTraceLogger : ITraceLogger
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000EE0A File Offset: 0x0000D00A
		private NullTraceLogger()
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000EE12 File Offset: 0x0000D012
		public void LogTraces(ITracer tracer)
		{
		}

		// Token: 0x04000337 RID: 823
		public static readonly NullTraceLogger Instance = new NullTraceLogger();
	}
}
