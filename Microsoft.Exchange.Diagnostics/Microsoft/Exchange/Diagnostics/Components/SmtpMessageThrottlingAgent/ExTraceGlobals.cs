using System;

namespace Microsoft.Exchange.Diagnostics.Components.SmtpMessageThrottlingAgent
{
	// Token: 0x02000386 RID: 902
	public static class ExTraceGlobals
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00055C14 File Offset: 0x00053E14
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

		// Token: 0x04001A22 RID: 6690
		private static Guid componentGuid = new Guid("B9416E03-DF78-4A00-87D7-48AEE982B9FE");

		// Token: 0x04001A23 RID: 6691
		private static Trace agentTracer = null;
	}
}
