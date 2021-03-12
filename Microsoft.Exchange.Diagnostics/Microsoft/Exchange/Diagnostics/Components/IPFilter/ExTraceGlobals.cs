using System;

namespace Microsoft.Exchange.Diagnostics.Components.IPFilter
{
	// Token: 0x020003CA RID: 970
	public static class ExTraceGlobals
	{
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00059A48 File Offset: 0x00057C48
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

		// Token: 0x04001BF5 RID: 7157
		private static Guid componentGuid = new Guid("5383B207-8388-4459-9124-B229AD7A64F3");

		// Token: 0x04001BF6 RID: 7158
		private static Trace agentTracer = null;
	}
}
