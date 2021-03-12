using System;

namespace Microsoft.Exchange.Diagnostics.Components.EDiscovery
{
	// Token: 0x02000403 RID: 1027
	public static class ExTraceGlobals
	{
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0005CD87 File Offset: 0x0005AF87
		public static Trace WebServiceTracer
		{
			get
			{
				if (ExTraceGlobals.webServiceTracer == null)
				{
					ExTraceGlobals.webServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.webServiceTracer;
			}
		}

		// Token: 0x04001D78 RID: 7544
		private static Guid componentGuid = new Guid("5BA6CF81-B765-4105-B94C-4FBA97C742C1");

		// Token: 0x04001D79 RID: 7545
		private static Trace webServiceTracer = null;
	}
}
