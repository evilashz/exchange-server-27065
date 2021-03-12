using System;

namespace Microsoft.Exchange.Diagnostics.Components.EOPScheduler
{
	// Token: 0x020003EC RID: 1004
	public static class ExTraceGlobals
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0005B7FB File Offset: 0x000599FB
		public static Trace EOPSchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.eOPSchedulerTracer == null)
				{
					ExTraceGlobals.eOPSchedulerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.eOPSchedulerTracer;
			}
		}

		// Token: 0x04001CD6 RID: 7382
		private static Guid componentGuid = new Guid("c33deb09-a5c1-4b6c-9339-a89b8786ae36");

		// Token: 0x04001CD7 RID: 7383
		private static Trace eOPSchedulerTracer = null;
	}
}
