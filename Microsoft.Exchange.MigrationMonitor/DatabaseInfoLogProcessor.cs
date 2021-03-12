using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000012 RID: 18
	internal class DatabaseInfoLogProcessor : StatsAndInfoCommonBaseLogProcessor
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00004000 File Offset: 0x00002200
		public DatabaseInfoLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, MigrationMonitor.MigrationMonitorContext.Config.GetConfig<string>("MbxDBStatsFolder")), "DBMailboxStats Log", MigrationMonitor.DatabaseInfoCsvSchemaInstance, MigrationMonitor.MigrationMonitorContext.Config.GetConfig<string>("DBStatsFileName"))
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000404F File Offset: 0x0000224F
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetDataBaseInfoUpdateTimestampV2";
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004056 File Offset: 0x00002256
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertDataBaseInfo_BatchUploadV3";
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000405D File Offset: 0x0000225D
		protected override string SqlParamName
		{
			get
			{
				return "DatabaseInfoList";
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004064 File Offset: 0x00002264
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.DatabaseInfoDataV3";
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000406B File Offset: 0x0000226B
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			return base.TryAddDatabaseIdKeyValue(dataRow);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004074 File Offset: 0x00002274
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating information to NewMan for database {0}. Will attempt again next cycle.", new object[]
			{
				base.CurrentDatabaseName
			});
			throw new UploadDatabaseInfoInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000040B4 File Offset: 0x000022B4
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing information log for database {0}", new object[]
			{
				base.CurrentDatabaseName
			});
		}

		// Token: 0x0400003B RID: 59
		public new const string KeyNameIsLogProcessorEnabled = "IsDatabaseInfoLogProcessorEnabled";

		// Token: 0x0400003C RID: 60
		public const string KeyNameMbxDBStatsFolder = "MbxDBStatsFolder";

		// Token: 0x0400003D RID: 61
		public const string DefaultMbxDBStatsFolder = "Logging\\CompleteMailboxStats";

		// Token: 0x0400003E RID: 62
		public const string KeyNameDBStatsFileName = "DBStatsFileName";

		// Token: 0x0400003F RID: 63
		public const string DefaultDBStatsFileNamePattern = "*DBStats*.log";

		// Token: 0x04000040 RID: 64
		private const string LogTypeName = "DBMailboxStats Log";
	}
}
