using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000099 RID: 153
	internal class MeetingMessageProcessingLog : QuickLog
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0001C443 File Offset: 0x0001A643
		public static void LogEntry(MailboxSession session, string entry, params object[] args)
		{
			MeetingMessageProcessingLog.instance.AppendFormatLogEntry(session, entry, args);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001C452 File Offset: 0x0001A652
		public static void LogEntry(MailboxSession session, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			MeetingMessageProcessingLog.instance.AppendFormatLogEntry(session, e, logWatsonReport, entry, args);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001C464 File Offset: 0x0001A664
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.MeetingMessageProcessingAgent.Log";
			}
		}

		// Token: 0x040002D7 RID: 727
		public const string MessageClass = "IPM.Microsoft.MeetingMessageProcessingAgent.Log";

		// Token: 0x040002D8 RID: 728
		private static MeetingMessageProcessingLog instance = new MeetingMessageProcessingLog();
	}
}
