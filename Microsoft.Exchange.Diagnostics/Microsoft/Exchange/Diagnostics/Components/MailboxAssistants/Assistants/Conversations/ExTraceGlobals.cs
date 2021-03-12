using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x0200035A RID: 858
	public static class ExTraceGlobals
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0005248A File Offset: 0x0005068A
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

		// Token: 0x04001888 RID: 6280
		private static Guid componentGuid = new Guid("85EA4EE7-EDB2-4A00-93D7-764949EF1225");

		// Token: 0x04001889 RID: 6281
		private static Trace generalTracer = null;
	}
}
