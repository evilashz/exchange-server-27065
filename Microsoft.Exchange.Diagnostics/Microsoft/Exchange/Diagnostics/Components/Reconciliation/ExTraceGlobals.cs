using System;

namespace Microsoft.Exchange.Diagnostics.Components.Reconciliation
{
	// Token: 0x02000312 RID: 786
	public static class ExTraceGlobals
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0004A860 File Offset: 0x00048A60
		public static PerfTrace StartProcessingMessagePerfTracer
		{
			get
			{
				if (ExTraceGlobals.startProcessingMessagePerfTracer == null)
				{
					ExTraceGlobals.startProcessingMessagePerfTracer = new PerfTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.startProcessingMessagePerfTracer;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0004A87E File Offset: 0x00048A7E
		public static PerfTrace EndProcessingMessagePerfTracer
		{
			get
			{
				if (ExTraceGlobals.endProcessingMessagePerfTracer == null)
				{
					ExTraceGlobals.endProcessingMessagePerfTracer = new PerfTrace(ExTraceGlobals.componentGuid, 255);
				}
				return ExTraceGlobals.endProcessingMessagePerfTracer;
			}
		}

		// Token: 0x04001506 RID: 5382
		private static Guid componentGuid = new Guid("E06E0123-1B5C-4f61-959D-8258BF6C689A");

		// Token: 0x04001507 RID: 5383
		private static PerfTrace startProcessingMessagePerfTracer = null;

		// Token: 0x04001508 RID: 5384
		private static PerfTrace endProcessingMessagePerfTracer = null;
	}
}
