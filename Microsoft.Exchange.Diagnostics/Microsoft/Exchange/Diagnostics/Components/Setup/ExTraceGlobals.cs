using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Setup
{
	// Token: 0x02000376 RID: 886
	public static class ExTraceGlobals
	{
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00054268 File Offset: 0x00052468
		public static Trace TraceTracer
		{
			get
			{
				if (ExTraceGlobals.traceTracer == null)
				{
					ExTraceGlobals.traceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.traceTracer;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00054286 File Offset: 0x00052486
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001967 RID: 6503
		private static Guid componentGuid = new Guid("0868076d-75ca-47bf-8d73-487edd017b4d");

		// Token: 0x04001968 RID: 6504
		private static Trace traceTracer = null;

		// Token: 0x04001969 RID: 6505
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
