using System;

namespace Microsoft.Exchange.Diagnostics.Components.OutboundSpamAlerting
{
	// Token: 0x020003D3 RID: 979
	public static class ExTraceGlobals
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00059E28 File Offset: 0x00058028
		public static Trace OutboundSpamAlertingTracer
		{
			get
			{
				if (ExTraceGlobals.outboundSpamAlertingTracer == null)
				{
					ExTraceGlobals.outboundSpamAlertingTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.outboundSpamAlertingTracer;
			}
		}

		// Token: 0x04001C15 RID: 7189
		private static Guid componentGuid = new Guid("53499FCE-2E79-4ECF-86E7-9F7C2FB652EC");

		// Token: 0x04001C16 RID: 7190
		private static Trace outboundSpamAlertingTracer = null;
	}
}
