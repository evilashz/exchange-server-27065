using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BD7 RID: 3031
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DelegateRulesManagementLogger : QuickLog
	{
		// Token: 0x06006BB5 RID: 27573 RVA: 0x001CD74E File Offset: 0x001CB94E
		public DelegateRulesManagementLogger() : base(200)
		{
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x001CD75B File Offset: 0x001CB95B
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.DelegateRulesManagement.Log";
			}
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x001CD762 File Offset: 0x001CB962
		public static void LogEntry(MailboxSession session, string info)
		{
			DelegateRulesManagementLogger.Logger.WriteLogEntry(session, string.Format("Timestamp: {0}   {1} ", DateTime.UtcNow, info));
		}

		// Token: 0x04003DA3 RID: 15779
		private const int MaxLogEntries = 200;

		// Token: 0x04003DA4 RID: 15780
		private const string RulesManagementLoggerString = "IPM.Microsoft.DelegateRulesManagement.Log";

		// Token: 0x04003DA5 RID: 15781
		private static DelegateRulesManagementLogger Logger = new DelegateRulesManagementLogger();
	}
}
