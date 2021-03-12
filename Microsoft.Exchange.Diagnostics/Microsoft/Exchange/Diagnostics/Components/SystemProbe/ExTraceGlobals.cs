using System;

namespace Microsoft.Exchange.Diagnostics.Components.SystemProbe
{
	// Token: 0x020003DA RID: 986
	public static class ExTraceGlobals
	{
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00059FE3 File Offset: 0x000581E3
		public static Trace ProbeTracer
		{
			get
			{
				if (ExTraceGlobals.probeTracer == null)
				{
					ExTraceGlobals.probeTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.probeTracer;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0005A001 File Offset: 0x00058201
		public static Trace AgentTracer
		{
			get
			{
				if (ExTraceGlobals.agentTracer == null)
				{
					ExTraceGlobals.agentTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.agentTracer;
			}
		}

		// Token: 0x04001C25 RID: 7205
		private static Guid componentGuid = new Guid("89ff0675-2089-453d-8c5a-21d9466a6eed");

		// Token: 0x04001C26 RID: 7206
		private static Trace probeTracer = null;

		// Token: 0x04001C27 RID: 7207
		private static Trace agentTracer = null;
	}
}
