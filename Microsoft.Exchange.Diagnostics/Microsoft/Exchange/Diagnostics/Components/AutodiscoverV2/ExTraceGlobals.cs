using System;

namespace Microsoft.Exchange.Diagnostics.Components.AutodiscoverV2
{
	// Token: 0x02000318 RID: 792
	public static class ExTraceGlobals
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0004AEF8 File Offset: 0x000490F8
		public static Trace LatencyTracer
		{
			get
			{
				if (ExTraceGlobals.latencyTracer == null)
				{
					ExTraceGlobals.latencyTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.latencyTracer;
			}
		}

		// Token: 0x04001537 RID: 5431
		private static Guid componentGuid = new Guid("7C505D81-27E6-4F26-8B18-5BA811425E5F");

		// Token: 0x04001538 RID: 5432
		private static Trace latencyTracer = null;
	}
}
