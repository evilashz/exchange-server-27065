using System;

namespace Microsoft.Exchange.Diagnostics.Components.EnvelopeFilter
{
	// Token: 0x020003C9 RID: 969
	public static class ExTraceGlobals
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00059A13 File Offset: 0x00057C13
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

		// Token: 0x04001BF3 RID: 7155
		private static Guid componentGuid = new Guid("93959C88-FC97-45C2-BCA9-D8A360CC07CF");

		// Token: 0x04001BF4 RID: 7156
		private static Trace agentTracer = null;
	}
}
