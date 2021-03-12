using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Common.Extensions
{
	// Token: 0x02000006 RID: 6
	internal class Tracer : ITracer
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000024CF File Offset: 0x000006CF
		public Tracer(Trace trace)
		{
			this.trace = trace;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024DE File Offset: 0x000006DE
		public bool IsErrorTraceEnabled
		{
			get
			{
				return this.trace.IsTraceEnabled(TraceType.ErrorTrace);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024EC File Offset: 0x000006EC
		public bool IsDebugTraceEnabled
		{
			get
			{
				return this.trace.IsTraceEnabled(TraceType.DebugTrace);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024FA File Offset: 0x000006FA
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.trace.TraceError(id, formatString, args);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000250A File Offset: 0x0000070A
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.trace.TraceDebug(id, formatString, args);
		}

		// Token: 0x04000008 RID: 8
		private Trace trace;
	}
}
