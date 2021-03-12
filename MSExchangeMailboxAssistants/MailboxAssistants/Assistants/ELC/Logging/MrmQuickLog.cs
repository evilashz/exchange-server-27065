using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging
{
	// Token: 0x020000AF RID: 175
	internal class MrmQuickLog : QuickLog
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x000337F8 File Offset: 0x000319F8
		public static void LogError(MailboxSession mailboxSession, Exception exception)
		{
			string entry = string.Format("{0} Exception: {1}", DateTime.UtcNow, exception);
			MrmQuickLog.Logger.WriteLogEntry(mailboxSession, entry);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00033827 File Offset: 0x00031A27
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.MRM.Log";
			}
		}

		// Token: 0x040004E8 RID: 1256
		private static readonly MrmQuickLog Logger = new MrmQuickLog();
	}
}
