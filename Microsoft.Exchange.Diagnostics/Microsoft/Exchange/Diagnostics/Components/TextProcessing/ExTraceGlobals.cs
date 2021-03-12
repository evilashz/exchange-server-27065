using System;

namespace Microsoft.Exchange.Diagnostics.Components.TextProcessing
{
	// Token: 0x020003C6 RID: 966
	public static class ExTraceGlobals
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0005980C File Offset: 0x00057A0C
		public static Trace SmartTrieTracer
		{
			get
			{
				if (ExTraceGlobals.smartTrieTracer == null)
				{
					ExTraceGlobals.smartTrieTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.smartTrieTracer;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0005982A File Offset: 0x00057A2A
		public static Trace MatcherTracer
		{
			get
			{
				if (ExTraceGlobals.matcherTracer == null)
				{
					ExTraceGlobals.matcherTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.matcherTracer;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00059848 File Offset: 0x00057A48
		public static Trace FingerprintTracer
		{
			get
			{
				if (ExTraceGlobals.fingerprintTracer == null)
				{
					ExTraceGlobals.fingerprintTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.fingerprintTracer;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00059866 File Offset: 0x00057A66
		public static Trace BoomerangTracer
		{
			get
			{
				if (ExTraceGlobals.boomerangTracer == null)
				{
					ExTraceGlobals.boomerangTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.boomerangTracer;
			}
		}

		// Token: 0x04001BE3 RID: 7139
		private static Guid componentGuid = new Guid("B15C3C00-9FF8-47B7-A975-70F1278017EF");

		// Token: 0x04001BE4 RID: 7140
		private static Trace smartTrieTracer = null;

		// Token: 0x04001BE5 RID: 7141
		private static Trace matcherTracer = null;

		// Token: 0x04001BE6 RID: 7142
		private static Trace fingerprintTracer = null;

		// Token: 0x04001BE7 RID: 7143
		private static Trace boomerangTracer = null;
	}
}
