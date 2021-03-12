using System;

namespace Microsoft.Exchange.Diagnostics.Components.OutboundIpReputationCheck
{
	// Token: 0x020003D8 RID: 984
	public static class ExTraceGlobals
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00059F31 File Offset: 0x00058131
		public static Trace OutboundIpReputationCheckTracer
		{
			get
			{
				if (ExTraceGlobals.outboundIpReputationCheckTracer == null)
				{
					ExTraceGlobals.outboundIpReputationCheckTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.outboundIpReputationCheckTracer;
			}
		}

		// Token: 0x04001C1F RID: 7199
		private static Guid componentGuid = new Guid("89ADA561-B705-4F12-8DCC-5370ACDEDC43");

		// Token: 0x04001C20 RID: 7200
		private static Trace outboundIpReputationCheckTracer = null;
	}
}
