using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service
{
	// Token: 0x0200039C RID: 924
	public static class ExTraceGlobals
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00057BC4 File Offset: 0x00055DC4
		public static Trace StartupShutdownTracer
		{
			get
			{
				if (ExTraceGlobals.startupShutdownTracer == null)
				{
					ExTraceGlobals.startupShutdownTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.startupShutdownTracer;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00057BE2 File Offset: 0x00055DE2
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001B06 RID: 6918
		private static Guid componentGuid = new Guid("2e177940-9c28-43b0-9f7a-b92bf03227a6");

		// Token: 0x04001B07 RID: 6919
		private static Trace startupShutdownTracer = null;

		// Token: 0x04001B08 RID: 6920
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
