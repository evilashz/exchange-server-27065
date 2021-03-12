using System;

namespace Microsoft.Exchange.Diagnostics.Components.RulesPublisher
{
	// Token: 0x020003D1 RID: 977
	public static class ExTraceGlobals
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00059D52 File Offset: 0x00057F52
		public static Trace RulesPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.rulesPublisherTracer == null)
				{
					ExTraceGlobals.rulesPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.rulesPublisherTracer;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00059D70 File Offset: 0x00057F70
		public static Trace IPListPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.iPListPublisherTracer == null)
				{
					ExTraceGlobals.iPListPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.iPListPublisherTracer;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00059D8E File Offset: 0x00057F8E
		public static Trace RulesProfilerTracer
		{
			get
			{
				if (ExTraceGlobals.rulesProfilerTracer == null)
				{
					ExTraceGlobals.rulesProfilerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.rulesProfilerTracer;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00059DAC File Offset: 0x00057FAC
		public static Trace SpamDataBlobPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.spamDataBlobPublisherTracer == null)
				{
					ExTraceGlobals.spamDataBlobPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.spamDataBlobPublisherTracer;
			}
		}

		// Token: 0x04001C0E RID: 7182
		private static Guid componentGuid = new Guid("0082B730-63A3-475E-B53C-ACCA6AFDC400");

		// Token: 0x04001C0F RID: 7183
		private static Trace rulesPublisherTracer = null;

		// Token: 0x04001C10 RID: 7184
		private static Trace iPListPublisherTracer = null;

		// Token: 0x04001C11 RID: 7185
		private static Trace rulesProfilerTracer = null;

		// Token: 0x04001C12 RID: 7186
		private static Trace spamDataBlobPublisherTracer = null;
	}
}
