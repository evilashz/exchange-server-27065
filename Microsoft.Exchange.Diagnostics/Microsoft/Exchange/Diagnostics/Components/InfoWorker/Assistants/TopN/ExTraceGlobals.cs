using System;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN
{
	// Token: 0x0200035C RID: 860
	public static class ExTraceGlobals
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00052560 File Offset: 0x00050760
		public static Trace TopNAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.topNAssistantTracer == null)
				{
					ExTraceGlobals.topNAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.topNAssistantTracer;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0005257E File Offset: 0x0005077E
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x0400188F RID: 6287
		private static Guid componentGuid = new Guid("E97F40AF-7C47-4d35-86A0-0725D7B8019D");

		// Token: 0x04001890 RID: 6288
		private static Trace topNAssistantTracer = null;

		// Token: 0x04001891 RID: 6289
		private static Trace pFDTracer = null;
	}
}
