using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000023 RID: 35
	internal class MRSFailureCsvSchema : BaseMrsMonitorCsvSchema
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000070AF File Offset: 0x000052AF
		public MRSFailureCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MRSFailureCsvSchema.requiredColumnsIds, MRSFailureCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MRSFailureCsvSchema.optionalColumnsIds, MRSFailureCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000070DB File Offset: 0x000052DB
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MRSFailureCsvSchema.requiredColumnsIds;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000070E2 File Offset: 0x000052E2
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MRSFailureCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000070E9 File Offset: 0x000052E9
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MRSFailureCsvSchema.optionalColumnsIds;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000070F0 File Offset: 0x000052F0
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MRSFailureCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x040000D3 RID: 211
		public const string FailureTypeColumn = "FailureType";

		// Token: 0x040000D4 RID: 212
		public const string FailureSideColumn = "FailureSide";

		// Token: 0x040000D5 RID: 213
		public const string SyncStageColumn = "SyncStage";

		// Token: 0x040000D6 RID: 214
		public const string OperationTypeColumn = "OperationType";

		// Token: 0x040000D7 RID: 215
		public const string IsFatalColumn = "IsFatal";

		// Token: 0x040000D8 RID: 216
		public const string StackTraceColumn = "StackTrace";

		// Token: 0x040000D9 RID: 217
		public const string AppVersionColumn = "AppVersion";

		// Token: 0x040000DA RID: 218
		public const string FailureGuidColumn = "FailureGuid";

		// Token: 0x040000DB RID: 219
		public const string FailureLevelColumn = "FailureLevel";

		// Token: 0x040000DC RID: 220
		public static readonly ColumnDefinition<int> WatsonHashColumn = new ColumnDefinition<int>("WatsonHash", "WatsonHashId", KnownStringType.WatsonHash);

		// Token: 0x040000DD RID: 221
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("FailureType", "FailureTypeId", KnownStringType.FailureType)
		};

		// Token: 0x040000DE RID: 222
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("RequestGuid")
		};

		// Token: 0x040000DF RID: 223
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("FailureSide", "FailureSideId", KnownStringType.FailureSide),
			new ColumnDefinition<int>("SyncStage", "SyncStageId", KnownStringType.RequestSyncStage),
			MRSFailureCsvSchema.WatsonHashColumn,
			new ColumnDefinition<int>("AppVersion", "AppVersionId", KnownStringType.AppVersion)
		};

		// Token: 0x040000E0 RID: 224
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<string>("OperationType"),
			new ColumnDefinition<bool>("IsFatal"),
			new ColumnDefinition<string>("StackTrace"),
			new ColumnDefinition<Guid>("FailureGuid"),
			new ColumnDefinition<int>("FailureLevel")
		};
	}
}
