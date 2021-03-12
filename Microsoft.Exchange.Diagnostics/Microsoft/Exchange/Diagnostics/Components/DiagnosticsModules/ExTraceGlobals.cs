using System;

namespace Microsoft.Exchange.Diagnostics.Components.DiagnosticsModules
{
	// Token: 0x02000374 RID: 884
	public static class ExTraceGlobals
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00054040 File Offset: 0x00052240
		public static Trace ErrorLoggingModuleTracer
		{
			get
			{
				if (ExTraceGlobals.errorLoggingModuleTracer == null)
				{
					ExTraceGlobals.errorLoggingModuleTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.errorLoggingModuleTracer;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0005405E File Offset: 0x0005225E
		public static Trace ClientDiagnosticsModuleTracer
		{
			get
			{
				if (ExTraceGlobals.clientDiagnosticsModuleTracer == null)
				{
					ExTraceGlobals.clientDiagnosticsModuleTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.clientDiagnosticsModuleTracer;
			}
		}

		// Token: 0x04001957 RID: 6487
		private static Guid componentGuid = new Guid("B79CCE07-AFC0-40CA-A6AD-4FB725D5770A");

		// Token: 0x04001958 RID: 6488
		private static Trace errorLoggingModuleTracer = null;

		// Token: 0x04001959 RID: 6489
		private static Trace clientDiagnosticsModuleTracer = null;
	}
}
