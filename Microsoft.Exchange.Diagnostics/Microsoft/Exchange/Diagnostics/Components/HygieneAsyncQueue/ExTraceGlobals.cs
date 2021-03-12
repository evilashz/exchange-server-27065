using System;

namespace Microsoft.Exchange.Diagnostics.Components.HygieneAsyncQueue
{
	// Token: 0x020003C0 RID: 960
	public static class ExTraceGlobals
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x000593A8 File Offset: 0x000575A8
		public static Trace AsyncQueueServiceTracer
		{
			get
			{
				if (ExTraceGlobals.asyncQueueServiceTracer == null)
				{
					ExTraceGlobals.asyncQueueServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.asyncQueueServiceTracer;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x000593C6 File Offset: 0x000575C6
		public static Trace AsyncQueueExecutorTracer
		{
			get
			{
				if (ExTraceGlobals.asyncQueueExecutorTracer == null)
				{
					ExTraceGlobals.asyncQueueExecutorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.asyncQueueExecutorTracer;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x000593E4 File Offset: 0x000575E4
		public static Trace AsyncStepExecutorTracer
		{
			get
			{
				if (ExTraceGlobals.asyncStepExecutorTracer == null)
				{
					ExTraceGlobals.asyncStepExecutorTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.asyncStepExecutorTracer;
			}
		}

		// Token: 0x04001BC1 RID: 7105
		private static Guid componentGuid = new Guid("040DF3E7-309C-4531-A762-6136DBD1004A");

		// Token: 0x04001BC2 RID: 7106
		private static Trace asyncQueueServiceTracer = null;

		// Token: 0x04001BC3 RID: 7107
		private static Trace asyncQueueExecutorTracer = null;

		// Token: 0x04001BC4 RID: 7108
		private static Trace asyncStepExecutorTracer = null;
	}
}
