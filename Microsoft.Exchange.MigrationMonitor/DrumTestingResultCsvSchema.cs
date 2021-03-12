using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000013 RID: 19
	internal class DrumTestingResultCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000040E8 File Offset: 0x000022E8
		public DrumTestingResultCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(DrumTestingResultCsvSchema.requiredColumnsIds, DrumTestingResultCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(DrumTestingResultCsvSchema.optionalColumnsIds, DrumTestingResultCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004114 File Offset: 0x00002314
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return DrumTestingResultCsvSchema.requiredColumnsIds;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000411B File Offset: 0x0000231B
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return DrumTestingResultCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004122 File Offset: 0x00002322
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return DrumTestingResultCsvSchema.optionalColumnsIds;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004129 File Offset: 0x00002329
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return DrumTestingResultCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x04000041 RID: 65
		public const string ExecutionGuidColumn = "ExecutionGuid";

		// Token: 0x04000042 RID: 66
		public const string TestTypeColumn = "TestType";

		// Token: 0x04000043 RID: 67
		public const string ObjectTypeColumn = "ObjectType";

		// Token: 0x04000044 RID: 68
		public const string IdentityColumn = "Identity";

		// Token: 0x04000045 RID: 69
		public const string FlagsColumn = "Flags";

		// Token: 0x04000046 RID: 70
		public const string StringIdColumn = "StringId";

		// Token: 0x04000047 RID: 71
		public const string ResultTypeColumn = "ResultType";

		// Token: 0x04000048 RID: 72
		public const string ResultCategoryColumn = "ResultCategory";

		// Token: 0x04000049 RID: 73
		public const string ResultDetailsColumn = "ResultDetails";

		// Token: 0x0400004A RID: 74
		public const string ResultHashColumn = "ResultHash";

		// Token: 0x0400004B RID: 75
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("TestType", "DrumTestingTestTypeId", KnownStringType.DrumTestingTestType)
		};

		// Token: 0x0400004C RID: 76
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("ExecutionGuid"),
			new ColumnDefinition<Guid>("Identity")
		};

		// Token: 0x0400004D RID: 77
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("ObjectType", "DrumTestingObjectTypeId", KnownStringType.DrumTestingObjectType),
			new ColumnDefinition<int>("ResultType", "DrumTestingResultTypeId", KnownStringType.DrumTestingResultType),
			new ColumnDefinition<int>("ResultCategory", "DrumTestingResultCategoryId", KnownStringType.DrumTestingResultCategoryType)
		};

		// Token: 0x0400004E RID: 78
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<int>("Flags"),
			new ColumnDefinition<string>("StringId"),
			new ColumnDefinition<string>("ResultDetails"),
			new ColumnDefinition<int>("ResultHash")
		};
	}
}
