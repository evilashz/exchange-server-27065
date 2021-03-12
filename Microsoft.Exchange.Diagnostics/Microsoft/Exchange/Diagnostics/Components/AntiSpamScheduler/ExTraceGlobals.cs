using System;

namespace Microsoft.Exchange.Diagnostics.Components.AntiSpamScheduler
{
	// Token: 0x020003ED RID: 1005
	public static class ExTraceGlobals
	{
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0005B830 File Offset: 0x00059A30
		public static Trace AntiSpamSchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.antiSpamSchedulerTracer == null)
				{
					ExTraceGlobals.antiSpamSchedulerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.antiSpamSchedulerTracer;
			}
		}

		// Token: 0x04001CD8 RID: 7384
		private static Guid componentGuid = new Guid("D1A9D40D-C69D-41F9-BB45-4E08192A6709");

		// Token: 0x04001CD9 RID: 7385
		private static Trace antiSpamSchedulerTracer = null;
	}
}
