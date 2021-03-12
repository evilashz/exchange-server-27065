using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharingFolder
{
	// Token: 0x0200034E RID: 846
	public static class ExTraceGlobals
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00051FAA File Offset: 0x000501AA
		public static Trace IsEventInterestingTracer
		{
			get
			{
				if (ExTraceGlobals.isEventInterestingTracer == null)
				{
					ExTraceGlobals.isEventInterestingTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.isEventInterestingTracer;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00051FC8 File Offset: 0x000501C8
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x0400185F RID: 6239
		private static Guid componentGuid = new Guid("B506E288-FC0D-4bf2-A8E7-ED0F43DC468F");

		// Token: 0x04001860 RID: 6240
		private static Trace isEventInterestingTracer = null;

		// Token: 0x04001861 RID: 6241
		private static Trace generalTracer = null;
	}
}
