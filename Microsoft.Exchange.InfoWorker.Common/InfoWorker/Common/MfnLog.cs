using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200001E RID: 30
	internal class MfnLog : QuickLog
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003AA9 File Offset: 0x00001CA9
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.MFN.Log";
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public static void LogEntry(MailboxSession session, string info)
		{
			MfnLog.Logger.WriteLogEntry(session, string.Format("{0} : {1}", DateTime.UtcNow, info));
		}

		// Token: 0x04000040 RID: 64
		private static MfnLog Logger = new MfnLog();
	}
}
