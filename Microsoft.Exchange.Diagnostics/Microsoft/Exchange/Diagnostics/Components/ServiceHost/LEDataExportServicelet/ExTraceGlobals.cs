using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.LEDataExportServicelet
{
	// Token: 0x020003B4 RID: 948
	public static class ExTraceGlobals
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00058C4C File Offset: 0x00056E4C
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x00058C6A File Offset: 0x00056E6A
		public static Trace ServiceletTracer
		{
			get
			{
				if (ExTraceGlobals.serviceletTracer == null)
				{
					ExTraceGlobals.serviceletTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serviceletTracer;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00058C88 File Offset: 0x00056E88
		public static Trace WorkerTracer
		{
			get
			{
				if (ExTraceGlobals.workerTracer == null)
				{
					ExTraceGlobals.workerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.workerTracer;
			}
		}

		// Token: 0x04001B87 RID: 7047
		private static Guid componentGuid = new Guid("f9dbde22-ed1e-4059-b757-51053ed786b8");

		// Token: 0x04001B88 RID: 7048
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B89 RID: 7049
		private static Trace serviceletTracer = null;

		// Token: 0x04001B8A RID: 7050
		private static Trace workerTracer = null;
	}
}
