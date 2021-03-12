using System;

namespace Microsoft.Exchange.Diagnostics.Components.OAB
{
	// Token: 0x020003E7 RID: 999
	public static class ExTraceGlobals
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0005B558 File Offset: 0x00059758
		public static Trace AssistantTracer
		{
			get
			{
				if (ExTraceGlobals.assistantTracer == null)
				{
					ExTraceGlobals.assistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantTracer;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0005B576 File Offset: 0x00059776
		public static Trace DataTracer
		{
			get
			{
				if (ExTraceGlobals.dataTracer == null)
				{
					ExTraceGlobals.dataTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dataTracer;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0005B594 File Offset: 0x00059794
		public static Trace RunNowTracer
		{
			get
			{
				if (ExTraceGlobals.runNowTracer == null)
				{
					ExTraceGlobals.runNowTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.runNowTracer;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0005B5B2 File Offset: 0x000597B2
		public static Trace HttpHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.httpHandlerTracer == null)
				{
					ExTraceGlobals.httpHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.httpHandlerTracer;
			}
		}

		// Token: 0x04001CC1 RID: 7361
		private static Guid componentGuid = new Guid("3934b4fd-72fb-44e1-b29a-6cac52257a5d");

		// Token: 0x04001CC2 RID: 7362
		private static Trace assistantTracer = null;

		// Token: 0x04001CC3 RID: 7363
		private static Trace dataTracer = null;

		// Token: 0x04001CC4 RID: 7364
		private static Trace runNowTracer = null;

		// Token: 0x04001CC5 RID: 7365
		private static Trace httpHandlerTracer = null;
	}
}
