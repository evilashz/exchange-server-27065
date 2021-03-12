using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D8C RID: 3468
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnonymousSharingLog : QuickLog
	{
		// Token: 0x06007768 RID: 30568 RVA: 0x0020EAEC File Offset: 0x0020CCEC
		public static void LogEntries(MailboxSession session, List<LocalizedString> entries)
		{
			foreach (LocalizedString value in entries)
			{
				AnonymousSharingLog.instance.AppendFormatLogEntry(session, value, new object[0]);
			}
		}

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06007769 RID: 30569 RVA: 0x0020EB4C File Offset: 0x0020CD4C
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.AnonymousSharing.Log";
			}
		}

		// Token: 0x040052AA RID: 21162
		private const string MessageClass = "IPM.Microsoft.AnonymousSharing.Log";

		// Token: 0x040052AB RID: 21163
		private static AnonymousSharingLog instance = new AnonymousSharingLog();
	}
}
