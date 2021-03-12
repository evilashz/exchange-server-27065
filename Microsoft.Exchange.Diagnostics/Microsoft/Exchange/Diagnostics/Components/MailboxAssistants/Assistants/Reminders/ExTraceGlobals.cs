using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200036C RID: 876
	public static class ExTraceGlobals
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00053CA0 File Offset: 0x00051EA0
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00053CBE File Offset: 0x00051EBE
		public static Trace HeuristicsTracer
		{
			get
			{
				if (ExTraceGlobals.heuristicsTracer == null)
				{
					ExTraceGlobals.heuristicsTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.heuristicsTracer;
			}
		}

		// Token: 0x04001939 RID: 6457
		private static Guid componentGuid = new Guid("2723E866-2665-4E65-A02E-4AD0BDDC3C5A");

		// Token: 0x0400193A RID: 6458
		private static Trace generalTracer = null;

		// Token: 0x0400193B RID: 6459
		private static Trace heuristicsTracer = null;
	}
}
