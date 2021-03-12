using System;

namespace Microsoft.Exchange.Diagnostics.Components.CommonLogging
{
	// Token: 0x02000322 RID: 802
	public static class ExTraceGlobals
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0004C1A2 File Offset: 0x0004A3A2
		public static Trace LogTracer
		{
			get
			{
				if (ExTraceGlobals.logTracer == null)
				{
					ExTraceGlobals.logTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.logTracer;
			}
		}

		// Token: 0x040015BE RID: 5566
		private static Guid componentGuid = new Guid("cdcd12e6-f300-4af2-ae55-7b090a8b9f50");

		// Token: 0x040015BF RID: 5567
		private static Trace logTracer = null;
	}
}
