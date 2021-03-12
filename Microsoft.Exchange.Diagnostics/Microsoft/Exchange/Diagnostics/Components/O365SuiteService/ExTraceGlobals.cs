using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.O365SuiteService
{
	// Token: 0x020003AA RID: 938
	public static class ExTraceGlobals
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000587B2 File Offset: 0x000569B2
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

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000587D0 File Offset: 0x000569D0
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

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000587EE File Offset: 0x000569EE
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

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0005880C File Offset: 0x00056A0C
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

		// Token: 0x04001B61 RID: 7009
		private static Guid componentGuid = new Guid("AF620BE4-41C6-4931-ABD7-B83FE584538D");

		// Token: 0x04001B62 RID: 7010
		private static Trace briefTracer = null;

		// Token: 0x04001B63 RID: 7011
		private static Trace verboseTracer = null;

		// Token: 0x04001B64 RID: 7012
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B65 RID: 7013
		private static Trace exceptionTracer = null;
	}
}
