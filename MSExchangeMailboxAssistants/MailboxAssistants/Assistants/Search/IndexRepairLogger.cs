using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x0200018E RID: 398
	internal class IndexRepairLogger
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0005CEEB File Offset: 0x0005B0EB
		public IndexRepairLogger()
		{
			this.log = new DiagnosticsLog(new DiagnosticsLogConfig(IndexRepairLogger.logDefaults), IndexRepairLogger.columns);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0005CF10 File Offset: 0x0005B110
		public void LogMailboxStatistics(MailboxStatistics mailboxStatistics)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0};{1};{2};{3};", new object[]
			{
				mailboxStatistics.Type.ToString(),
				mailboxStatistics.ErrorStatistics.DocumentCount,
				mailboxStatistics.StoreStatistics.DocumentCount,
				mailboxStatistics.StoreStatistics.DeletedDocumentCount
			});
			foreach (KeyValuePair<EvaluationErrors, long> keyValuePair in mailboxStatistics.ErrorStatistics.FailedDocuments)
			{
				stringBuilder.AppendFormat("{0}={1}:", keyValuePair.Key, keyValuePair.Value);
			}
			this.Append(new object[]
			{
				null,
				2,
				"MailboxStatistics",
				mailboxStatistics.DatabaseName,
				mailboxStatistics.MailboxGuid,
				stringBuilder.ToString()
			});
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0005D02C File Offset: 0x0005B22C
		protected virtual void Append(params object[] values)
		{
			this.log.Append(values);
		}

		// Token: 0x04000A00 RID: 2560
		internal const int LogVersion = 2;

		// Token: 0x04000A01 RID: 2561
		internal const string MailboxStatisticsEventId = "MailboxStatistics";

		// Token: 0x04000A02 RID: 2562
		private static readonly DiagnosticsLogConfig.LogDefaults logDefaults = new DiagnosticsLogConfig.LogDefaults(Guid.Parse("2650f1d0-3bf3-4954-99b7-3ec397ca0174"), "Search", "Search Index Repair Assistant", Path.Combine(ExchangeSetupContext.InstallPath, "Logging", "Search", "IndexRepairAssistant"), "IndexRepairAssistant_", "SearchLogs");

		// Token: 0x04000A03 RID: 2563
		private static readonly string[] columns = new string[]
		{
			"date-time",
			"version",
			"entryId",
			"database-name",
			"mailbox-guid",
			"operation"
		};

		// Token: 0x04000A04 RID: 2564
		private readonly DiagnosticsLog log;
	}
}
