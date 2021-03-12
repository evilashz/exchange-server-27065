using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Diagnostics
{
	// Token: 0x02000313 RID: 787
	public static class ExTraceGlobals
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0004A8BD File Offset: 0x00048ABD
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004A8DB File Offset: 0x00048ADB
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001509 RID: 5385
		private static Guid componentGuid = new Guid("20e99398-d277-4ead-acde-0dbe119f7ce6");

		// Token: 0x0400150A RID: 5386
		private static Trace commonTracer = null;

		// Token: 0x0400150B RID: 5387
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
