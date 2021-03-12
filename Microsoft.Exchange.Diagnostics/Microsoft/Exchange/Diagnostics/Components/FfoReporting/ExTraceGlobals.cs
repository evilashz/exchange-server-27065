using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.FfoReporting
{
	// Token: 0x020003F6 RID: 1014
	public static class ExTraceGlobals
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0005BEF6 File Offset: 0x0005A0F6
		public static Trace CmdletsTracer
		{
			get
			{
				if (ExTraceGlobals.cmdletsTracer == null)
				{
					ExTraceGlobals.cmdletsTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.cmdletsTracer;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0005BF14 File Offset: 0x0005A114
		public static Trace HttpModuleTracer
		{
			get
			{
				if (ExTraceGlobals.httpModuleTracer == null)
				{
					ExTraceGlobals.httpModuleTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.httpModuleTracer;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0005BF32 File Offset: 0x0005A132
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001D0C RID: 7436
		private static Guid componentGuid = new Guid("68B388E3-66FC-486C-BD59-C1738D89D4D7");

		// Token: 0x04001D0D RID: 7437
		private static Trace cmdletsTracer = null;

		// Token: 0x04001D0E RID: 7438
		private static Trace httpModuleTracer = null;

		// Token: 0x04001D0F RID: 7439
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
