using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200012B RID: 299
	internal class RbaLog : QuickLog
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		public static void LogEntry(MailboxSession session, MessageItem message, EvaluationResults state)
		{
			string entry = string.Format("Action:{0}, Sender: {1}, From: {2}, Subject :{3} Internet Message Id: {4}", new object[]
			{
				state,
				(message.Sender != null) ? message.Sender.EmailAddress : "null",
				(message.From != null) ? message.From.EmailAddress : "null",
				message.Subject,
				message.InternetMessageId
			});
			RbaLog.Logger.AppendFormatLogEntry(session, entry, new object[0]);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0004E337 File Offset: 0x0004C537
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.RBA.Log";
			}
		}

		// Token: 0x0400076C RID: 1900
		private static RbaLog Logger = new RbaLog();
	}
}
