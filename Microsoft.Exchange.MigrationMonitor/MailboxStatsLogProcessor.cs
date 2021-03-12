using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000018 RID: 24
	internal class MailboxStatsLogProcessor : StatsAndInfoCommonBaseLogProcessor
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00004888 File Offset: 0x00002A88
		public MailboxStatsLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, MigrationMonitor.MigrationMonitorContext.Config.GetConfig<string>("MbxDBStatsFolder")), "MailboxStats Log", MigrationMonitor.MailboxStatsCsvSchemaInstance, MigrationMonitor.MigrationMonitorContext.Config.GetConfig<string>("MbxStatsFileName"))
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000048D7 File Offset: 0x00002AD7
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetMailboxStatsUpdateTimestampV2";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000048DE File Offset: 0x00002ADE
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMailboxStatsV5";
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000048E5 File Offset: 0x00002AE5
		protected override string SqlParamName
		{
			get
			{
				return "MailboxStatsList";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000048EC File Offset: 0x00002AEC
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MailboxStatsDataV5";
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000048F4 File Offset: 0x00002AF4
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating mailbox stats to NewMan for database {0}. Will attempt again next cycle.", new object[]
			{
				base.CurrentDatabaseName
			});
			throw new UploadMailboxStatsInBatchFailureException(ex.InnerException);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004934 File Offset: 0x00002B34
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing mailbox stats log, mailbox guid is {0}", new object[]
			{
				MigMonUtilities.GetColumnValue<Guid>(row, "MailboxGuid")
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004974 File Offset: 0x00002B74
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			if (!base.TryAddDatabaseIdKeyValue(dataRow))
			{
				return false;
			}
			string errorString = string.Format("Error parsing mailbox stats log, mailbox guid is {0} and its mailbox type is empty", MigMonUtilities.GetColumnValue<Guid>(row, "MailboxGuid"));
			string errorString2 = string.Format("Error parsing mailbox stats log, mailbox guid is {0} and its disconnectReason is invalid", MigMonUtilities.GetColumnValue<Guid>(row, "MailboxGuid"));
			base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.DisconnectReason, errorString2, true);
			return base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.MailboxType, errorString, false);
		}

		// Token: 0x0400008A RID: 138
		public new const string KeyNameIsLogProcessorEnabled = "IsMailboxStatsLogProcessorEnabled";

		// Token: 0x0400008B RID: 139
		public const string KeyNameMbxStatsFileName = "MbxStatsFileName";

		// Token: 0x0400008C RID: 140
		public const string DefaultMbxStatsFileNamePattern = "*MbxStats*.log";

		// Token: 0x0400008D RID: 141
		private const string LogTypeName = "MailboxStats Log";
	}
}
