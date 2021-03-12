using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x0200034C RID: 844
	public static class ExTraceGlobals
	{
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00051F1C File Offset: 0x0005011C
		public static Trace CalendarRepairAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.calendarRepairAssistantTracer == null)
				{
					ExTraceGlobals.calendarRepairAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.calendarRepairAssistantTracer;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00051F3A File Offset: 0x0005013A
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x0400185A RID: 6234
		private static Guid componentGuid = new Guid("80005C8C-8262-4BB9-B6DA-F288D9C542E4");

		// Token: 0x0400185B RID: 6235
		private static Trace calendarRepairAssistantTracer = null;

		// Token: 0x0400185C RID: 6236
		private static Trace pFDTracer = null;
	}
}
