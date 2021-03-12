using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Tasks
{
	// Token: 0x0200036D RID: 877
	public static class ExTraceGlobals
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00053CF9 File Offset: 0x00051EF9
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

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00053D17 File Offset: 0x00051F17
		public static Trace LogTracer
		{
			get
			{
				if (ExTraceGlobals.logTracer == null)
				{
					ExTraceGlobals.logTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.logTracer;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00053D35 File Offset: 0x00051F35
		public static Trace ErrorTracer
		{
			get
			{
				if (ExTraceGlobals.errorTracer == null)
				{
					ExTraceGlobals.errorTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.errorTracer;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00053D53 File Offset: 0x00051F53
		public static Trace EventTracer
		{
			get
			{
				if (ExTraceGlobals.eventTracer == null)
				{
					ExTraceGlobals.eventTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.eventTracer;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00053D71 File Offset: 0x00051F71
		public static Trace EnterExitTracer
		{
			get
			{
				if (ExTraceGlobals.enterExitTracer == null)
				{
					ExTraceGlobals.enterExitTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.enterExitTracer;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00053D8F File Offset: 0x00051F8F
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x0400193C RID: 6460
		private static Guid componentGuid = new Guid("1e254b9e-d663-4138-8183-e5e4b077f8d3");

		// Token: 0x0400193D RID: 6461
		private static Trace traceTracer = null;

		// Token: 0x0400193E RID: 6462
		private static Trace logTracer = null;

		// Token: 0x0400193F RID: 6463
		private static Trace errorTracer = null;

		// Token: 0x04001940 RID: 6464
		private static Trace eventTracer = null;

		// Token: 0x04001941 RID: 6465
		private static Trace enterExitTracer = null;

		// Token: 0x04001942 RID: 6466
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
