using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x0200034F RID: 847
	public static class ExTraceGlobals
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00052003 File Offset: 0x00050203
		public static Trace AssistantTracer
		{
			get
			{
				if (ExTraceGlobals.assistantTracer == null)
				{
					ExTraceGlobals.assistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantTracer;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00052021 File Offset: 0x00050221
		public static Trace SystemMailboxTracer
		{
			get
			{
				if (ExTraceGlobals.systemMailboxTracer == null)
				{
					ExTraceGlobals.systemMailboxTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.systemMailboxTracer;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0005203F File Offset: 0x0005023F
		public static Trace UserSettingsTracer
		{
			get
			{
				if (ExTraceGlobals.userSettingsTracer == null)
				{
					ExTraceGlobals.userSettingsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.userSettingsTracer;
			}
		}

		// Token: 0x04001862 RID: 6242
		private static Guid componentGuid = new Guid("B6075A29-F2B0-4B58-8791-8869F07F732A");

		// Token: 0x04001863 RID: 6243
		private static Trace assistantTracer = null;

		// Token: 0x04001864 RID: 6244
		private static Trace systemMailboxTracer = null;

		// Token: 0x04001865 RID: 6245
		private static Trace userSettingsTracer = null;
	}
}
