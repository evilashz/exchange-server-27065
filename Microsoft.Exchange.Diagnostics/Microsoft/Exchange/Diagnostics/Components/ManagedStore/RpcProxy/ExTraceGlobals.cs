using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy
{
	// Token: 0x0200039D RID: 925
	public static class ExTraceGlobals
	{
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00057C1E File Offset: 0x00055E1E
		public static Trace ProxyAdminTracer
		{
			get
			{
				if (ExTraceGlobals.proxyAdminTracer == null)
				{
					ExTraceGlobals.proxyAdminTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.proxyAdminTracer;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00057C3C File Offset: 0x00055E3C
		public static Trace ProxyMapiTracer
		{
			get
			{
				if (ExTraceGlobals.proxyMapiTracer == null)
				{
					ExTraceGlobals.proxyMapiTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.proxyMapiTracer;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x00057C5A File Offset: 0x00055E5A
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

		// Token: 0x04001B09 RID: 6921
		private static Guid componentGuid = new Guid("437834ff-ce93-406a-be6e-4547009136c8");

		// Token: 0x04001B0A RID: 6922
		private static Trace proxyAdminTracer = null;

		// Token: 0x04001B0B RID: 6923
		private static Trace proxyMapiTracer = null;

		// Token: 0x04001B0C RID: 6924
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
