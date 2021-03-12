using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200002F RID: 47
	internal class WLMResourceStatsCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000085E6 File Offset: 0x000067E6
		public WLMResourceStatsCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(WLMResourceStatsCsvSchema.requiredColumnsIds, WLMResourceStatsCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(WLMResourceStatsCsvSchema.optionalColumnsIds, WLMResourceStatsCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008614 File Offset: 0x00006814
		public override DataTable GetCsvSchemaDataTable()
		{
			return base.GetCsvSchemaDataTable();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008629 File Offset: 0x00006829
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return WLMResourceStatsCsvSchema.requiredColumnsIds;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008630 File Offset: 0x00006830
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return WLMResourceStatsCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008637 File Offset: 0x00006837
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return WLMResourceStatsCsvSchema.optionalColumnsIds;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000863E File Offset: 0x0000683E
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return WLMResourceStatsCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x04000110 RID: 272
		public const string OwnerResourceNameColumnName = "OwnerResourceName";

		// Token: 0x04000111 RID: 273
		public const string OwnerResourceTypeColumnName = "OwnerResourceType";

		// Token: 0x04000112 RID: 274
		public const string ResourceKeyColumnName = "ResourceKey";

		// Token: 0x04000113 RID: 275
		public const string LoadStateColumnName = "LoadState";

		// Token: 0x04000114 RID: 276
		public const string LoadRatioColumnName = "LoadRatio";

		// Token: 0x04000115 RID: 277
		public const string MetricColumnName = "Metric";

		// Token: 0x04000116 RID: 278
		public const string DynamicCapacityColumnName = "DynamicCapacity";

		// Token: 0x04000117 RID: 279
		public const string UnderloadedInFiveMinColumnName = "UnderloadedIn5Min";

		// Token: 0x04000118 RID: 280
		public const string FullInFiveMinColumnName = "FullIn5Min";

		// Token: 0x04000119 RID: 281
		public const string OverloadedInFiveMinColumnName = "OverloadedIn5Min";

		// Token: 0x0400011A RID: 282
		public const string CriticalInFiveMinColumnName = "CriticalIn5Min";

		// Token: 0x0400011B RID: 283
		public const string UnknownInFiveMinColumnName = "UnknownIn5Min";

		// Token: 0x0400011C RID: 284
		public const string UnderloadedInHourColumnName = "UnderloadedIn1Hour";

		// Token: 0x0400011D RID: 285
		public const string FullInHourColumnName = "FullIn1Hour";

		// Token: 0x0400011E RID: 286
		public const string OverloadedInHourColumnName = "OverloadedIn1Hour";

		// Token: 0x0400011F RID: 287
		public const string CriticalInHourColumnName = "CriticalIn1Hour";

		// Token: 0x04000120 RID: 288
		public const string UnknownInHourColumnName = "UnknownIn1Hour";

		// Token: 0x04000121 RID: 289
		public const string UnderloadedInDayColumnName = "UnderloadedIn1Day";

		// Token: 0x04000122 RID: 290
		public const string FullInDayColumnName = "FullIn1Day";

		// Token: 0x04000123 RID: 291
		public const string OverloadedInDayColumnName = "OverloadedIn1Day";

		// Token: 0x04000124 RID: 292
		public const string CriticalInDayColumnName = "CriticalIn1Day";

		// Token: 0x04000125 RID: 293
		public const string UnknownInDayColumnName = "UnknownIn1Day";

		// Token: 0x04000126 RID: 294
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("OwnerResourceName", "OwnerResourceNameID", KnownStringType.OwnerResourceNameType),
			new ColumnDefinition<int>("OwnerResourceType", "OwnerResourceTypeID", KnownStringType.OwnerResourceTypeType),
			new ColumnDefinition<int>("ResourceKey", "ResourceKeyID", KnownStringType.ResourceKeyType),
			new ColumnDefinition<int>("LoadState", "LoadStateID", KnownStringType.LoadStateType)
		};

		// Token: 0x04000127 RID: 295
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<float>("LoadRatio"),
			new ColumnDefinition<int>("DynamicCapacity"),
			new ColumnDefinition<int>("UnderloadedIn5Min"),
			new ColumnDefinition<int>("FullIn5Min"),
			new ColumnDefinition<int>("OverloadedIn5Min"),
			new ColumnDefinition<int>("CriticalIn5Min"),
			new ColumnDefinition<int>("UnknownIn5Min"),
			new ColumnDefinition<int>("UnderloadedIn1Hour"),
			new ColumnDefinition<int>("FullIn1Hour"),
			new ColumnDefinition<int>("OverloadedIn1Hour"),
			new ColumnDefinition<int>("CriticalIn1Hour"),
			new ColumnDefinition<int>("UnknownIn1Hour"),
			new ColumnDefinition<int>("UnderloadedIn1Day"),
			new ColumnDefinition<int>("FullIn1Day"),
			new ColumnDefinition<int>("OverloadedIn1Day"),
			new ColumnDefinition<int>("CriticalIn1Day"),
			new ColumnDefinition<int>("UnknownIn1Day")
		};

		// Token: 0x04000128 RID: 296
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x04000129 RID: 297
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<int>("Metric")
		};
	}
}
