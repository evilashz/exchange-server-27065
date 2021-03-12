using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000277 RID: 631
	internal class SharingLog : QuickLog
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x000546E8 File Offset: 0x000528E8
		public static void LogEntry(MailboxSession session, string entry)
		{
			string entry2 = string.Format("{0}, Mailbox: '{1}', Entry {2}.", DateTime.UtcNow.ToString(), session.MailboxOwner.LegacyDn, entry);
			SharingLog.instance.WriteLogEntry(session, entry2);
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0005472B File Offset: 0x0005292B
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.Sharing.Log";
			}
		}

		// Token: 0x04000BDF RID: 3039
		public const string MessageClass = "IPM.Microsoft.Sharing.Log";

		// Token: 0x04000BE0 RID: 3040
		private static SharingLog instance = new SharingLog();
	}
}
