using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch
{
	// Token: 0x02000342 RID: 834
	public static class ExTraceGlobals
	{
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0005139F File Offset: 0x0004F59F
		public static Trace RequestRoutingTracer
		{
			get
			{
				if (ExTraceGlobals.requestRoutingTracer == null)
				{
					ExTraceGlobals.requestRoutingTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.requestRoutingTracer;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000513BD File Offset: 0x0004F5BD
		public static Trace DistributionListHandlingTracer
		{
			get
			{
				if (ExTraceGlobals.distributionListHandlingTracer == null)
				{
					ExTraceGlobals.distributionListHandlingTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.distributionListHandlingTracer;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000513DB File Offset: 0x0004F5DB
		public static Trace ProxyWebRequestTracer
		{
			get
			{
				if (ExTraceGlobals.proxyWebRequestTracer == null)
				{
					ExTraceGlobals.proxyWebRequestTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.proxyWebRequestTracer;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x000513F9 File Offset: 0x0004F5F9
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x00051417 File Offset: 0x0004F617
		public static Trace GetFolderRequestTracer
		{
			get
			{
				if (ExTraceGlobals.getFolderRequestTracer == null)
				{
					ExTraceGlobals.getFolderRequestTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.getFolderRequestTracer;
			}
		}

		// Token: 0x04001805 RID: 6149
		private static Guid componentGuid = new Guid("92915F00-6982-4d61-818A-6931EBA87182");

		// Token: 0x04001806 RID: 6150
		private static Trace requestRoutingTracer = null;

		// Token: 0x04001807 RID: 6151
		private static Trace distributionListHandlingTracer = null;

		// Token: 0x04001808 RID: 6152
		private static Trace proxyWebRequestTracer = null;

		// Token: 0x04001809 RID: 6153
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x0400180A RID: 6154
		private static Trace getFolderRequestTracer = null;
	}
}
