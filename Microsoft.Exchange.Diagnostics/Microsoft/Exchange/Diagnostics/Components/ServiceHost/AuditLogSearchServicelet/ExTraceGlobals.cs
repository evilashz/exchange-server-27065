using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.AuditLogSearchServicelet
{
	// Token: 0x020003BA RID: 954
	public static class ExTraceGlobals
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00059043 File Offset: 0x00057243
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

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00059061 File Offset: 0x00057261
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

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0005907F File Offset: 0x0005727F
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

		// Token: 0x04001BA6 RID: 7078
		private static Guid componentGuid = new Guid("9cff9e83-a0b3-4110-bcd8-617e9ea1e0fe");

		// Token: 0x04001BA7 RID: 7079
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001BA8 RID: 7080
		private static Trace serviceletTracer = null;

		// Token: 0x04001BA9 RID: 7081
		private static Trace workerTracer = null;
	}
}
