using System;

namespace Microsoft.Exchange.Diagnostics.Components.ExtendedReportWeb
{
	// Token: 0x020003E6 RID: 998
	public static class ExTraceGlobals
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0005B523 File Offset: 0x00059723
		public static Trace ExtendedReportWebTracer
		{
			get
			{
				if (ExTraceGlobals.extendedReportWebTracer == null)
				{
					ExTraceGlobals.extendedReportWebTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.extendedReportWebTracer;
			}
		}

		// Token: 0x04001CBF RID: 7359
		private static Guid componentGuid = new Guid("5d38a9b6-081e-4f9e-992a-4f818692c421");

		// Token: 0x04001CC0 RID: 7360
		private static Trace extendedReportWebTracer = null;
	}
}
