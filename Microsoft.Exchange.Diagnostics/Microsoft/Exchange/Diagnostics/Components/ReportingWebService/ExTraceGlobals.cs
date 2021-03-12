using System;

namespace Microsoft.Exchange.Diagnostics.Components.ReportingWebService
{
	// Token: 0x020003E8 RID: 1000
	public static class ExTraceGlobals
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x0005B5F9 File Offset: 0x000597F9
		public static Trace ReportingWebServiceTracer
		{
			get
			{
				if (ExTraceGlobals.reportingWebServiceTracer == null)
				{
					ExTraceGlobals.reportingWebServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.reportingWebServiceTracer;
			}
		}

		// Token: 0x04001CC6 RID: 7366
		private static Guid componentGuid = new Guid("E1C7EC5C-4B42-427A-9033-BC062E34DD7F");

		// Token: 0x04001CC7 RID: 7367
		private static Trace reportingWebServiceTracer = null;
	}
}
