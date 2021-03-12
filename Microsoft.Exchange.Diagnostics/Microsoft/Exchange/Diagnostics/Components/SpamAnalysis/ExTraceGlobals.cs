using System;

namespace Microsoft.Exchange.Diagnostics.Components.SpamAnalysis
{
	// Token: 0x020003F3 RID: 1011
	public static class ExTraceGlobals
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0005BDEB File Offset: 0x00059FEB
		public static Trace SmtpReceiveAgentTracer
		{
			get
			{
				if (ExTraceGlobals.smtpReceiveAgentTracer == null)
				{
					ExTraceGlobals.smtpReceiveAgentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.smtpReceiveAgentTracer;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0005BE09 File Offset: 0x0005A009
		public static Trace RoutingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.routingAgentTracer == null)
				{
					ExTraceGlobals.routingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.routingAgentTracer;
			}
		}

		// Token: 0x04001D03 RID: 7427
		private static Guid componentGuid = new Guid("31331149-AA27-4F2D-9B69-5B46ED4ED829");

		// Token: 0x04001D04 RID: 7428
		private static Trace smtpReceiveAgentTracer = null;

		// Token: 0x04001D05 RID: 7429
		private static Trace routingAgentTracer = null;
	}
}
