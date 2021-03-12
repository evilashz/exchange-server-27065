using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.HttpProxy
{
	// Token: 0x020003A9 RID: 937
	public static class ExTraceGlobals
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00058711 File Offset: 0x00056911
		public static Trace BriefTracer
		{
			get
			{
				if (ExTraceGlobals.briefTracer == null)
				{
					ExTraceGlobals.briefTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.briefTracer;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005872F File Offset: 0x0005692F
		public static Trace VerboseTracer
		{
			get
			{
				if (ExTraceGlobals.verboseTracer == null)
				{
					ExTraceGlobals.verboseTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.verboseTracer;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0005874D File Offset: 0x0005694D
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005876B File Offset: 0x0005696B
		public static Trace ExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.exceptionTracer == null)
				{
					ExTraceGlobals.exceptionTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.exceptionTracer;
			}
		}

		// Token: 0x04001B5C RID: 7004
		private static Guid componentGuid = new Guid("1b54246f-70bf-4885-90b7-800205abb16c");

		// Token: 0x04001B5D RID: 7005
		private static Trace briefTracer = null;

		// Token: 0x04001B5E RID: 7006
		private static Trace verboseTracer = null;

		// Token: 0x04001B5F RID: 7007
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B60 RID: 7008
		private static Trace exceptionTracer = null;
	}
}
