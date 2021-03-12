using System;

namespace Microsoft.Exchange.Diagnostics.Components.ThrottlingService
{
	// Token: 0x020003B3 RID: 947
	public static class ExTraceGlobals
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00058BCF File Offset: 0x00056DCF
		public static Trace ThrottlingServiceTracer
		{
			get
			{
				if (ExTraceGlobals.throttlingServiceTracer == null)
				{
					ExTraceGlobals.throttlingServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.throttlingServiceTracer;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00058BED File Offset: 0x00056DED
		public static Trace ThrottlingClientTracer
		{
			get
			{
				if (ExTraceGlobals.throttlingClientTracer == null)
				{
					ExTraceGlobals.throttlingClientTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.throttlingClientTracer;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00058C0B File Offset: 0x00056E0B
		public static Trace ExportTracer
		{
			get
			{
				if (ExTraceGlobals.exportTracer == null)
				{
					ExTraceGlobals.exportTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.exportTracer;
			}
		}

		// Token: 0x04001B83 RID: 7043
		private static Guid componentGuid = new Guid("2e888ec1-6dd9-48cb-aa14-5bf7cad71a88");

		// Token: 0x04001B84 RID: 7044
		private static Trace throttlingServiceTracer = null;

		// Token: 0x04001B85 RID: 7045
		private static Trace throttlingClientTracer = null;

		// Token: 0x04001B86 RID: 7046
		private static Trace exportTracer = null;
	}
}
