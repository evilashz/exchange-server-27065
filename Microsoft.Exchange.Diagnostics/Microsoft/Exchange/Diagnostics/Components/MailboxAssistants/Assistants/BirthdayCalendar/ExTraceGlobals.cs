using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.BirthdayCalendar
{
	// Token: 0x02000347 RID: 839
	public static class ExTraceGlobals
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0005196E File Offset: 0x0004FB6E
		public static Trace BirthdayAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayAssistantTracer == null)
				{
					ExTraceGlobals.birthdayAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.birthdayAssistantTracer;
			}
		}

		// Token: 0x04001830 RID: 6192
		private static Guid componentGuid = new Guid("62FDC2FC-DE0F-413E-9526-2ACEDC1D3D45");

		// Token: 0x04001831 RID: 6193
		private static Trace birthdayAssistantTracer = null;
	}
}
