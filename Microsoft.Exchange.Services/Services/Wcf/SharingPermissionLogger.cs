using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000971 RID: 2417
	internal class SharingPermissionLogger : QuickLog
	{
		// Token: 0x06004570 RID: 17776 RVA: 0x000F3AF7 File Offset: 0x000F1CF7
		public SharingPermissionLogger() : base(100)
		{
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06004571 RID: 17777 RVA: 0x000F3B01 File Offset: 0x000F1D01
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.SharingPermissionManagement.Log";
			}
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000F3B08 File Offset: 0x000F1D08
		public static void LogEntry(MailboxSession session, string info, string ContextIdentifier)
		{
			SharingPermissionLogger.Logger.WriteLogEntry(session, string.Format("Timestamp: {0} Logging Id: {1} : {2}", DateTime.UtcNow, ContextIdentifier, info));
		}

		// Token: 0x04002863 RID: 10339
		private const int MaxLogEntries = 100;

		// Token: 0x04002864 RID: 10340
		public const string SharingPermissionLoggerString = "IPM.Microsoft.SharingPermissionManagement.Log";

		// Token: 0x04002865 RID: 10341
		private static SharingPermissionLogger Logger = new SharingPermissionLogger();
	}
}
