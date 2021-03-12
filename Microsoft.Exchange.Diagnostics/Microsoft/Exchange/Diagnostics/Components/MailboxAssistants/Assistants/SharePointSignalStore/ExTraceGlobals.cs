using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000368 RID: 872
	public static class ExTraceGlobals
	{
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00053BCC File Offset: 0x00051DCC
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

		// Token: 0x04001931 RID: 6449
		private static Guid componentGuid = new Guid("2bb0e55b-1550-4d7e-896d-febef4aaf74c");

		// Token: 0x04001932 RID: 6450
		private static Trace generalTracer = null;
	}
}
