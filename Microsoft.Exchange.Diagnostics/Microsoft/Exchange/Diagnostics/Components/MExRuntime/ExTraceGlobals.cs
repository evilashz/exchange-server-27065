using System;

namespace Microsoft.Exchange.Diagnostics.Components.MExRuntime
{
	// Token: 0x02000323 RID: 803
	public static class ExTraceGlobals
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0004C1D7 File Offset: 0x0004A3D7
		public static Trace InitializeTracer
		{
			get
			{
				if (ExTraceGlobals.initializeTracer == null)
				{
					ExTraceGlobals.initializeTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.initializeTracer;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0004C1F5 File Offset: 0x0004A3F5
		public static Trace DispatchTracer
		{
			get
			{
				if (ExTraceGlobals.dispatchTracer == null)
				{
					ExTraceGlobals.dispatchTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dispatchTracer;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0004C213 File Offset: 0x0004A413
		public static Trace ShutdownTracer
		{
			get
			{
				if (ExTraceGlobals.shutdownTracer == null)
				{
					ExTraceGlobals.shutdownTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.shutdownTracer;
			}
		}

		// Token: 0x040015C0 RID: 5568
		private static Guid componentGuid = new Guid("b7916055-456d-46f6-bdd2-42ac88ccb655");

		// Token: 0x040015C1 RID: 5569
		private static Trace initializeTracer = null;

		// Token: 0x040015C2 RID: 5570
		private static Trace dispatchTracer = null;

		// Token: 0x040015C3 RID: 5571
		private static Trace shutdownTracer = null;
	}
}
