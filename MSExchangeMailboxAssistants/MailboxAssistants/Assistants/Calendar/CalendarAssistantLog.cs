using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000B4 RID: 180
	internal class CalendarAssistantLog : QuickLog
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00035614 File Offset: 0x00033814
		public static void LogEntry(MailboxSession session, string entry, params object[] args)
		{
			CalendarAssistantLog.instance.AppendFormatLogEntry(session, entry, args);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00035623 File Offset: 0x00033823
		public static void LogEntry(MailboxSession session, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			CalendarAssistantLog.instance.AppendFormatLogEntry(session, e, logWatsonReport, entry, args);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00035635 File Offset: 0x00033835
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.Calendar.Log";
			}
		}

		// Token: 0x04000582 RID: 1410
		public const string MessageClass = "IPM.Microsoft.Calendar.Log";

		// Token: 0x04000583 RID: 1411
		private static CalendarAssistantLog instance = new CalendarAssistantLog();
	}
}
