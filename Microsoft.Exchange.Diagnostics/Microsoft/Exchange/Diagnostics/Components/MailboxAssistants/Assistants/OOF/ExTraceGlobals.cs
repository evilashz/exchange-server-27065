using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.OOF
{
	// Token: 0x02000348 RID: 840
	public static class ExTraceGlobals
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000519A3 File Offset: 0x0004FBA3
		public static Trace MailboxDataTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxDataTracer == null)
				{
					ExTraceGlobals.mailboxDataTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.mailboxDataTracer;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000519C1 File Offset: 0x0004FBC1
		public static Trace OofSettingsHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.oofSettingsHandlerTracer == null)
				{
					ExTraceGlobals.oofSettingsHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.oofSettingsHandlerTracer;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x000519DF File Offset: 0x0004FBDF
		public static Trace LegacyOofReplyTemplateHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.legacyOofReplyTemplateHandlerTracer == null)
				{
					ExTraceGlobals.legacyOofReplyTemplateHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.legacyOofReplyTemplateHandlerTracer;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x000519FD File Offset: 0x0004FBFD
		public static Trace LegacyOofStateHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.legacyOofStateHandlerTracer == null)
				{
					ExTraceGlobals.legacyOofStateHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.legacyOofStateHandlerTracer;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00051A1B File Offset: 0x0004FC1B
		public static Trace OofSchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.oofSchedulerTracer == null)
				{
					ExTraceGlobals.oofSchedulerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.oofSchedulerTracer;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00051A39 File Offset: 0x0004FC39
		public static Trace OofScheduledActionTracer
		{
			get
			{
				if (ExTraceGlobals.oofScheduledActionTracer == null)
				{
					ExTraceGlobals.oofScheduledActionTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.oofScheduledActionTracer;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00051A57 File Offset: 0x0004FC57
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00051A75 File Offset: 0x0004FC75
		public static Trace OofScheduleStoreTracer
		{
			get
			{
				if (ExTraceGlobals.oofScheduleStoreTracer == null)
				{
					ExTraceGlobals.oofScheduleStoreTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.oofScheduleStoreTracer;
			}
		}

		// Token: 0x04001832 RID: 6194
		private static Guid componentGuid = new Guid("EF72E07E-3E86-4250-9083-C18CD673631D");

		// Token: 0x04001833 RID: 6195
		private static Trace mailboxDataTracer = null;

		// Token: 0x04001834 RID: 6196
		private static Trace oofSettingsHandlerTracer = null;

		// Token: 0x04001835 RID: 6197
		private static Trace legacyOofReplyTemplateHandlerTracer = null;

		// Token: 0x04001836 RID: 6198
		private static Trace legacyOofStateHandlerTracer = null;

		// Token: 0x04001837 RID: 6199
		private static Trace oofSchedulerTracer = null;

		// Token: 0x04001838 RID: 6200
		private static Trace oofScheduledActionTracer = null;

		// Token: 0x04001839 RID: 6201
		private static Trace pFDTracer = null;

		// Token: 0x0400183A RID: 6202
		private static Trace oofScheduleStoreTracer = null;
	}
}
