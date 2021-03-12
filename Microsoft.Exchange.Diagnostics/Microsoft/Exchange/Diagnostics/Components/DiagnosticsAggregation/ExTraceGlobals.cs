using System;

namespace Microsoft.Exchange.Diagnostics.Components.DiagnosticsAggregation
{
	// Token: 0x020003BB RID: 955
	public static class ExTraceGlobals
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000590C0 File Offset: 0x000572C0
		public static Trace DiagnosticsAggregationTracer
		{
			get
			{
				if (ExTraceGlobals.diagnosticsAggregationTracer == null)
				{
					ExTraceGlobals.diagnosticsAggregationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.diagnosticsAggregationTracer;
			}
		}

		// Token: 0x04001BAA RID: 7082
		private static Guid componentGuid = new Guid("3e5245bb-9b29-457c-9cbf-83294dcb9a64");

		// Token: 0x04001BAB RID: 7083
		private static Trace diagnosticsAggregationTracer = null;
	}
}
