using System;

namespace Microsoft.Exchange.Diagnostics.Components.RemotePowershellBackendCmdletProxy
{
	// Token: 0x02000373 RID: 883
	public static class ExTraceGlobals
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0005400B File Offset: 0x0005220B
		public static Trace RemotePowershellBackendCmdletProxyModuleTracer
		{
			get
			{
				if (ExTraceGlobals.remotePowershellBackendCmdletProxyModuleTracer == null)
				{
					ExTraceGlobals.remotePowershellBackendCmdletProxyModuleTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.remotePowershellBackendCmdletProxyModuleTracer;
			}
		}

		// Token: 0x04001955 RID: 6485
		private static Guid componentGuid = new Guid("A4E8E535-4D59-49CC-B92D-4598367E5B35");

		// Token: 0x04001956 RID: 6486
		private static Trace remotePowershellBackendCmdletProxyModuleTracer = null;
	}
}
