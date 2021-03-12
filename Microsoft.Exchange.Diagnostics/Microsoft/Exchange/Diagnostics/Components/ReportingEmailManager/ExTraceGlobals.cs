using System;

namespace Microsoft.Exchange.Diagnostics.Components.ReportingEmailManager
{
	// Token: 0x02000405 RID: 1029
	public static class ExTraceGlobals
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0005CDF1 File Offset: 0x0005AFF1
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x04001D7C RID: 7548
		private static Guid componentGuid = new Guid("be2f13aa-6b0e-40b0-8612-74f560f2a53c");

		// Token: 0x04001D7D RID: 7549
		private static Trace commonTracer = null;
	}
}
