using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000247 RID: 583
	internal interface IRemindersAssistantLog
	{
		// Token: 0x060015E1 RID: 5601
		void LogEntry(IMailboxSession session, string entry, params object[] args);

		// Token: 0x060015E2 RID: 5602
		void LogEntry(IMailboxSession session, Exception e, bool logWatsonReport, string entry, params object[] args);
	}
}
