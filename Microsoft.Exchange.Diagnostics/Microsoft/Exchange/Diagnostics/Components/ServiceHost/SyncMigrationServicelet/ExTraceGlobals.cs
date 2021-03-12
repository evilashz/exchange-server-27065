using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet
{
	// Token: 0x020003B2 RID: 946
	public static class ExTraceGlobals
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00058B76 File Offset: 0x00056D76
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

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00058B94 File Offset: 0x00056D94
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

		// Token: 0x04001B80 RID: 7040
		private static Guid componentGuid = new Guid("b99de8a0-2fff-48b7-8b56-cfc3119f8f0a");

		// Token: 0x04001B81 RID: 7041
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B82 RID: 7042
		private static Trace serviceletTracer = null;
	}
}
