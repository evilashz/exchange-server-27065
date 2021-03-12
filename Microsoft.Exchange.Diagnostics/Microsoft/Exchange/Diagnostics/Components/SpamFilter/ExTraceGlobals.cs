using System;

namespace Microsoft.Exchange.Diagnostics.Components.SpamFilter
{
	// Token: 0x020003C4 RID: 964
	public static class ExTraceGlobals
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0005962C File Offset: 0x0005782C
		public static Trace AgentTracer
		{
			get
			{
				if (ExTraceGlobals.agentTracer == null)
				{
					ExTraceGlobals.agentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.agentTracer;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0005964A File Offset: 0x0005784A
		public static Trace BlockSendersTracer
		{
			get
			{
				if (ExTraceGlobals.blockSendersTracer == null)
				{
					ExTraceGlobals.blockSendersTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.blockSendersTracer;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00059668 File Offset: 0x00057868
		public static Trace SafeSendersTracer
		{
			get
			{
				if (ExTraceGlobals.safeSendersTracer == null)
				{
					ExTraceGlobals.safeSendersTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.safeSendersTracer;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x00059686 File Offset: 0x00057886
		public static Trace BypassCheckTracer
		{
			get
			{
				if (ExTraceGlobals.bypassCheckTracer == null)
				{
					ExTraceGlobals.bypassCheckTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.bypassCheckTracer;
			}
		}

		// Token: 0x04001BD5 RID: 7125
		private static Guid componentGuid = new Guid("175562D6-54D7-4C59-A421-598E03755639");

		// Token: 0x04001BD6 RID: 7126
		private static Trace agentTracer = null;

		// Token: 0x04001BD7 RID: 7127
		private static Trace blockSendersTracer = null;

		// Token: 0x04001BD8 RID: 7128
		private static Trace safeSendersTracer = null;

		// Token: 0x04001BD9 RID: 7129
		private static Trace bypassCheckTracer = null;
	}
}
