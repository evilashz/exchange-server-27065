using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000027 RID: 39
	internal class MRSWorkloadAvailabilityCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public MRSWorkloadAvailabilityCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MRSWorkloadAvailabilityCsvSchema.requiredColumnsIds, MRSWorkloadAvailabilityCsvSchema.requiredColumnsAsIs, "Time"), null, null)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public override DataTable GetCsvSchemaDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					{
						this.TimeStampColumnName,
						typeof(SqlDateTime)
					},
					{
						"LoggingServerId",
						typeof(int)
					},
					{
						"Version",
						typeof(int)
					},
					{
						"WorkloadTypeId",
						typeof(int)
					},
					{
						"Availability",
						typeof(float)
					}
				}
			};
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007B6C File Offset: 0x00005D6C
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MRSWorkloadAvailabilityCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007B73 File Offset: 0x00005D73
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MRSWorkloadAvailabilityCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007B7A File Offset: 0x00005D7A
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return new List<ColumnDefinition<int>>();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007B81 File Offset: 0x00005D81
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return new List<IColumnDefinition>();
		}

		// Token: 0x040000E8 RID: 232
		public const string ServerColumn = "Server";

		// Token: 0x040000E9 RID: 233
		public const string VersionColumn = "Version";

		// Token: 0x040000EA RID: 234
		public const string EventContextColumn = "EventContext";

		// Token: 0x040000EB RID: 235
		public const string EventDataColumn = "EventData";

		// Token: 0x040000EC RID: 236
		public const string WorkloadTypeIdColumn = "WorkloadTypeId";

		// Token: 0x040000ED RID: 237
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x040000EE RID: 238
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<object>("Server"),
			new ColumnDefinition<object>("Version"),
			new ColumnDefinition<object>("EventContext"),
			new ColumnDefinition<object>("EventData")
		};
	}
}
