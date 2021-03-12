using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.MailboxProcessor
{
	// Token: 0x0200036A RID: 874
	public static class ExTraceGlobals
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00053C36 File Offset: 0x00051E36
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

		// Token: 0x04001935 RID: 6453
		private static Guid componentGuid = new Guid("401BDE63-12DA-45E8-A634-FCE43B2617B6");

		// Token: 0x04001936 RID: 6454
		private static Trace generalTracer = null;
	}
}
