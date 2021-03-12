using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet
{
	// Token: 0x020003B1 RID: 945
	public static class ExTraceGlobals
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00058AF9 File Offset: 0x00056CF9
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00058B17 File Offset: 0x00056D17
		public static Trace ServiceletTracer
		{
			get
			{
				if (ExTraceGlobals.serviceletTracer == null)
				{
					ExTraceGlobals.serviceletTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serviceletTracer;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00058B35 File Offset: 0x00056D35
		public static Trace WorkerTracer
		{
			get
			{
				if (ExTraceGlobals.workerTracer == null)
				{
					ExTraceGlobals.workerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.workerTracer;
			}
		}

		// Token: 0x04001B7C RID: 7036
		private static Guid componentGuid = new Guid("9132698f-5149-4949-a24f-1bb1928f9692");

		// Token: 0x04001B7D RID: 7037
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B7E RID: 7038
		private static Trace serviceletTracer = null;

		// Token: 0x04001B7F RID: 7039
		private static Trace workerTracer = null;
	}
}
