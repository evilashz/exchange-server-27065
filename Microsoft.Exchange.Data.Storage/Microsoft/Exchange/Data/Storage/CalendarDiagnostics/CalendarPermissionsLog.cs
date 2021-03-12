using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000371 RID: 881
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarPermissionsLog : QuickLog
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x0009BE28 File Offset: 0x0009A028
		public CalendarPermissionsLog() : base(200)
		{
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x0009BE35 File Offset: 0x0009A035
		public static void LogEntry(MailboxSession session, string entry, params object[] args)
		{
			CalendarPermissionsLog.instance.AppendFormatLogEntry(session, entry, args);
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x0009BE44 File Offset: 0x0009A044
		public static void LogEntry(MailboxSession session, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			CalendarPermissionsLog.instance.AppendFormatLogEntry(session, e, logWatsonReport, entry, args);
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x0009BE56 File Offset: 0x0009A056
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.CalendarPermissions.Log";
			}
		}

		// Token: 0x0400171E RID: 5918
		public const string MessageClass = "IPM.Microsoft.CalendarPermissions.Log";

		// Token: 0x0400171F RID: 5919
		private const int MaxLogEntries = 200;

		// Token: 0x04001720 RID: 5920
		private static CalendarPermissionsLog instance = new CalendarPermissionsLog();
	}
}
