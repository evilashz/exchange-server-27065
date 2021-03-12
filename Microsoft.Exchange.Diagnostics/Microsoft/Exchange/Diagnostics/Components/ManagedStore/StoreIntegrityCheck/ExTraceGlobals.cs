using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck
{
	// Token: 0x0200039E RID: 926
	public static class ExTraceGlobals
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00057C9C File Offset: 0x00055E9C
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

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00057CBA File Offset: 0x00055EBA
		public static Trace OnlineIsintegTracer
		{
			get
			{
				if (ExTraceGlobals.onlineIsintegTracer == null)
				{
					ExTraceGlobals.onlineIsintegTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.onlineIsintegTracer;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00057CD8 File Offset: 0x00055ED8
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

		// Token: 0x04001B0D RID: 6925
		private static Guid componentGuid = new Guid("856DA9F3-E7F6-4565-84F6-71A96AF18D92");

		// Token: 0x04001B0E RID: 6926
		private static Trace startupShutdownTracer = null;

		// Token: 0x04001B0F RID: 6927
		private static Trace onlineIsintegTracer = null;

		// Token: 0x04001B10 RID: 6928
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
