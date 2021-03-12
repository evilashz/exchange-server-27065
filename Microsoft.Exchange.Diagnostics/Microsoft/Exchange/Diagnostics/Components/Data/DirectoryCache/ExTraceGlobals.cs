using System;

namespace Microsoft.Exchange.Diagnostics.Components.Data.DirectoryCache
{
	// Token: 0x020003F9 RID: 1017
	public static class ExTraceGlobals
	{
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005C001 File Offset: 0x0005A201
		public static Trace SessionTracer
		{
			get
			{
				if (ExTraceGlobals.sessionTracer == null)
				{
					ExTraceGlobals.sessionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.sessionTracer;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0005C01F File Offset: 0x0005A21F
		public static Trace CacheSessionTracer
		{
			get
			{
				if (ExTraceGlobals.cacheSessionTracer == null)
				{
					ExTraceGlobals.cacheSessionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.cacheSessionTracer;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0005C03D File Offset: 0x0005A23D
		public static Trace WCFServiceEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.wCFServiceEndpointTracer == null)
				{
					ExTraceGlobals.wCFServiceEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.wCFServiceEndpointTracer;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005C05B File Offset: 0x0005A25B
		public static Trace WCFClientEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.wCFClientEndpointTracer == null)
				{
					ExTraceGlobals.wCFClientEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.wCFClientEndpointTracer;
			}
		}

		// Token: 0x04001D15 RID: 7445
		private static Guid componentGuid = new Guid("2550C2A5-C4F4-4358-83E4-894A370B5A20");

		// Token: 0x04001D16 RID: 7446
		private static Trace sessionTracer = null;

		// Token: 0x04001D17 RID: 7447
		private static Trace cacheSessionTracer = null;

		// Token: 0x04001D18 RID: 7448
		private static Trace wCFServiceEndpointTracer = null;

		// Token: 0x04001D19 RID: 7449
		private static Trace wCFClientEndpointTracer = null;
	}
}
