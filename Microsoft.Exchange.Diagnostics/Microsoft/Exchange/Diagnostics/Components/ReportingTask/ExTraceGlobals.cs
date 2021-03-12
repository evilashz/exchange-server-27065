using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ReportingTask
{
	// Token: 0x020003E3 RID: 995
	public static class ExTraceGlobals
	{
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0005B09A File Offset: 0x0005929A
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

		// Token: 0x04001C9E RID: 7326
		private static Guid componentGuid = new Guid("d2050d6b-1713-4b3c-86d3-cbe8f21c5cf3");

		// Token: 0x04001C9F RID: 7327
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
