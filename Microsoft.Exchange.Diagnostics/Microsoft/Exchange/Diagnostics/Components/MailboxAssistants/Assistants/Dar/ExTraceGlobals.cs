using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Dar
{
	// Token: 0x02000350 RID: 848
	public static class ExTraceGlobals
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00052080 File Offset: 0x00050280
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

		// Token: 0x04001866 RID: 6246
		private static Guid componentGuid = new Guid("5790DD8E-22E8-451E-BCA4-0AF9F5C69A4E");

		// Token: 0x04001867 RID: 6247
		private static Trace generalTracer = null;
	}
}
