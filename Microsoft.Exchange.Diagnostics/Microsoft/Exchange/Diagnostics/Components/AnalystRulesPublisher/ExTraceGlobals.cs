using System;

namespace Microsoft.Exchange.Diagnostics.Components.AnalystRulesPublisher
{
	// Token: 0x020003D2 RID: 978
	public static class ExTraceGlobals
	{
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00059DF3 File Offset: 0x00057FF3
		public static Trace AnalystRulesPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.analystRulesPublisherTracer == null)
				{
					ExTraceGlobals.analystRulesPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.analystRulesPublisherTracer;
			}
		}

		// Token: 0x04001C13 RID: 7187
		private static Guid componentGuid = new Guid("A6B672FA-759B-4090-B7B8-83F450061F2B");

		// Token: 0x04001C14 RID: 7188
		private static Trace analystRulesPublisherTracer = null;
	}
}
