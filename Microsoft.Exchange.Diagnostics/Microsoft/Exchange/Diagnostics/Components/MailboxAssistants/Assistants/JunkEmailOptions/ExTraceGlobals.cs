using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000355 RID: 853
	public static class ExTraceGlobals
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00052285 File Offset: 0x00050485
		public static Trace JEOAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.jEOAssistantTracer == null)
				{
					ExTraceGlobals.jEOAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.jEOAssistantTracer;
			}
		}

		// Token: 0x04001877 RID: 6263
		private static Guid componentGuid = new Guid("F0C5D2BC-B9E3-4095-B124-2AF4940121D1");

		// Token: 0x04001878 RID: 6264
		private static Trace jEOAssistantTracer = null;
	}
}
