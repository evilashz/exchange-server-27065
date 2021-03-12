using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000021 RID: 33
	internal class MRSBadItemCsvSchema : BaseMrsMonitorCsvSchema
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00006DCF File Offset: 0x00004FCF
		public MRSBadItemCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MRSBadItemCsvSchema.requiredColumnsIds, MRSBadItemCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MRSBadItemCsvSchema.optionalColumnsIds, MRSBadItemCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006DFC File Offset: 0x00004FFC
		public override DataTable GetCsvSchemaDataTable()
		{
			DataTable csvSchemaDataTable = base.GetCsvSchemaDataTable();
			csvSchemaDataTable.Columns.Add("BadItemKind", typeof(string));
			csvSchemaDataTable.Columns.Add("WKFType", typeof(string));
			csvSchemaDataTable.Columns.Add("MessageClass", typeof(string));
			csvSchemaDataTable.Columns.Add("Category", typeof(string));
			return csvSchemaDataTable;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006E7D File Offset: 0x0000507D
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MRSBadItemCsvSchema.requiredColumnsIds;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006E84 File Offset: 0x00005084
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MRSBadItemCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006E8B File Offset: 0x0000508B
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MRSBadItemCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006E92 File Offset: 0x00005092
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MRSBadItemCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x040000C9 RID: 201
		public const string BadItemKindColumn = "BadItemKind";

		// Token: 0x040000CA RID: 202
		public const string CategoryColumn = "Category";

		// Token: 0x040000CB RID: 203
		public const string WKFTypeColumn = "WKFType";

		// Token: 0x040000CC RID: 204
		public const string MessageClassColumn = "MessageClass";

		// Token: 0x040000CD RID: 205
		public const string FailureMessageColumn = "FailureMessage";

		// Token: 0x040000CE RID: 206
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x040000CF RID: 207
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("RequestGuid")
		};

		// Token: 0x040000D0 RID: 208
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("BadItemKind", "BadItemKindId", KnownStringType.BadItemKind),
			new ColumnDefinition<int>("WKFType", "WkfTypeId", KnownStringType.BadItemWkfTypeId),
			new ColumnDefinition<int>("MessageClass", "MessageClassId", KnownStringType.BadItemMessageClass),
			new ColumnDefinition<int>("Category", "CategoryId", KnownStringType.BadItemCategory)
		};

		// Token: 0x040000D1 RID: 209
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<string>("FailureMessage"),
			new ColumnDefinition<string>("EntryId"),
			new ColumnDefinition<string>("FolderId"),
			new ColumnDefinition<long>("MessageSize"),
			new ColumnDefinition<SqlDateTime>("DateSent"),
			new ColumnDefinition<SqlDateTime>("DateReceived")
		};
	}
}
