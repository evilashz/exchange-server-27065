using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C2 RID: 194
	internal class SharingSyncAssistantLog : QuickLog
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x00039E02 File Offset: 0x00038002
		public static void LogEntry(MailboxSession session, string entry, params object[] args)
		{
			SharingSyncAssistantLog.instance.AppendFormatLogEntry(session, entry, args);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00039E11 File Offset: 0x00038011
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.SharingSyncAssistant.Log";
			}
		}

		// Token: 0x040005D2 RID: 1490
		public const string MessageClass = "IPM.Microsoft.SharingSyncAssistant.Log";

		// Token: 0x040005D3 RID: 1491
		private static SharingSyncAssistantLog instance = new SharingSyncAssistantLog();
	}
}
