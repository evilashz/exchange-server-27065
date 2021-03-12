using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync
{
	// Token: 0x0200034D RID: 845
	public static class ExTraceGlobals
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00051F75 File Offset: 0x00050175
		public static Trace CalendarSyncAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.calendarSyncAssistantTracer == null)
				{
					ExTraceGlobals.calendarSyncAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.calendarSyncAssistantTracer;
			}
		}

		// Token: 0x0400185D RID: 6237
		private static Guid componentGuid = new Guid("21C31235-EE44-4c3d-9876-9F91AC6F8EAA");

		// Token: 0x0400185E RID: 6238
		private static Trace calendarSyncAssistantTracer = null;
	}
}
