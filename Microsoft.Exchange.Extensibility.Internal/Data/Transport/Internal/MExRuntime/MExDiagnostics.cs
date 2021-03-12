using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MExRuntime;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200008A RID: 138
	internal static class MExDiagnostics
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x000147CC File Offset: 0x000129CC
		public static ExEventLog EventLog
		{
			get
			{
				return MExDiagnostics.logger;
			}
		}

		// Token: 0x040004B4 RID: 1204
		private static readonly ExEventLog logger = new ExEventLog(ExTraceGlobals.DispatchTracer.Category, "MSExchange Extensibility");
	}
}
