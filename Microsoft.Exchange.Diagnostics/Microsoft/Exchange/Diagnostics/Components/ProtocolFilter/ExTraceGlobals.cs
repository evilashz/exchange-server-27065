using System;

namespace Microsoft.Exchange.Diagnostics.Components.ProtocolFilter
{
	// Token: 0x02000382 RID: 898
	public static class ExTraceGlobals
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00055924 File Offset: 0x00053B24
		public static Trace SenderFilterAgentTracer
		{
			get
			{
				if (ExTraceGlobals.senderFilterAgentTracer == null)
				{
					ExTraceGlobals.senderFilterAgentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.senderFilterAgentTracer;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00055942 File Offset: 0x00053B42
		public static Trace RecipientFilterAgentTracer
		{
			get
			{
				if (ExTraceGlobals.recipientFilterAgentTracer == null)
				{
					ExTraceGlobals.recipientFilterAgentTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.recipientFilterAgentTracer;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00055960 File Offset: 0x00053B60
		public static Trace OtherTracer
		{
			get
			{
				if (ExTraceGlobals.otherTracer == null)
				{
					ExTraceGlobals.otherTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.otherTracer;
			}
		}

		// Token: 0x04001A0B RID: 6667
		private static Guid componentGuid = new Guid("0C5B72B3-290E-4c06-BE9D-D4727DF5FC0D");

		// Token: 0x04001A0C RID: 6668
		private static Trace senderFilterAgentTracer = null;

		// Token: 0x04001A0D RID: 6669
		private static Trace recipientFilterAgentTracer = null;

		// Token: 0x04001A0E RID: 6670
		private static Trace otherTracer = null;
	}
}
