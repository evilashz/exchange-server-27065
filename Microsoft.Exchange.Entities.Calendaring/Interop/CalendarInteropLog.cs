using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000061 RID: 97
	internal class CalendarInteropLog : QuickLog, ICalendarInteropLog
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00009BE4 File Offset: 0x00007DE4
		public static CalendarInteropLog Default
		{
			get
			{
				return CalendarInteropLog.DefaultInstance;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009BEB File Offset: 0x00007DEB
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.CalendarInterop.Log";
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public void LogEntry(IStoreSession session, string entry, params object[] args)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null)
			{
				this.LogMessageEntry(mailboxSession, entry, args);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009C14 File Offset: 0x00007E14
		public void LogEntry(IStoreSession session, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null)
			{
				this.LogExceptionEntry(mailboxSession, e, logWatsonReport, entry, args);
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009C38 File Offset: 0x00007E38
		public void SafeLogEntry(IStoreSession session, Exception exceptionToReport, bool logWatsonReport, string entry, params object[] args)
		{
			try
			{
				this.LogEntry(session, exceptionToReport, logWatsonReport, entry, args);
			}
			catch (Exception arg)
			{
				ExTraceGlobals.CalendarInteropTracer.TraceError<Exception>(0L, "Error writing CalendarInteropLog: {0}", arg);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009C7C File Offset: 0x00007E7C
		protected virtual void LogMessageEntry(MailboxSession mailboxSession, string entry, params object[] args)
		{
			base.AppendFormatLogEntry(mailboxSession, entry, args);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009C87 File Offset: 0x00007E87
		protected virtual void LogExceptionEntry(MailboxSession mailboxSession, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			base.AppendFormatLogEntry(mailboxSession, e, logWatsonReport, entry, args);
		}

		// Token: 0x040000AE RID: 174
		public const string MessageClass = "IPM.Microsoft.CalendarInterop.Log";

		// Token: 0x040000AF RID: 175
		private static readonly CalendarInteropLog DefaultInstance = new CalendarInteropLog();
	}
}
