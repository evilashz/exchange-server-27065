using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Search
{
	// Token: 0x0200036B RID: 875
	public static class ExTraceGlobals
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00053C6B File Offset: 0x00051E6B
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

		// Token: 0x04001937 RID: 6455
		private static Guid componentGuid = new Guid("48B0B47F-5559-44A4-ACAE-7B55C77BBB2D");

		// Token: 0x04001938 RID: 6456
		private static Trace generalTracer = null;
	}
}
