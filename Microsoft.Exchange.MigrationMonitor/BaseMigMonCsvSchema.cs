using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000004 RID: 4
	internal abstract class BaseMigMonCsvSchema : CsvSchema
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002DCD File Offset: 0x00000FCD
		protected BaseMigMonCsvSchema(Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(int.MaxValue, requiredColumns, optionalColumns, prohibitedColumns)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002DDD File Offset: 0x00000FDD
		public virtual string TimeStampColumnName
		{
			get
			{
				return "Time";
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E34 File Offset: 0x00001034
		public virtual DataTable GetCsvSchemaDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(this.TimeStampColumnName, typeof(SqlDateTime));
			dataTable.Columns.Add("LoggingServerId", typeof(int));
			this.GetRequiredColumnsIds().ForEach(delegate(ColumnDefinition<int> rc)
			{
				dataTable.Columns.Add(rc.DataTableKeyColumnName, typeof(int));
			});
			foreach (IColumnDefinition columnDefinition in this.GetRequiredColumnsAsIs())
			{
				dataTable.Columns.Add(columnDefinition.ColumnName, columnDefinition.ColumnType);
			}
			this.GetOptionalColumnsIds().ForEach(delegate(ColumnDefinition<int> oc)
			{
				dataTable.Columns.Add(oc.DataTableKeyColumnName, typeof(int));
			});
			foreach (IColumnDefinition columnDefinition2 in this.GetOptionalColumnsAsIs())
			{
				dataTable.Columns.Add(columnDefinition2.ColumnName, columnDefinition2.ColumnType);
			}
			return dataTable;
		}

		// Token: 0x0600002A RID: 42
		public abstract List<ColumnDefinition<int>> GetRequiredColumnsIds();

		// Token: 0x0600002B RID: 43
		public abstract List<IColumnDefinition> GetRequiredColumnsAsIs();

		// Token: 0x0600002C RID: 44
		public abstract List<ColumnDefinition<int>> GetOptionalColumnsIds();

		// Token: 0x0600002D RID: 45
		public abstract List<IColumnDefinition> GetOptionalColumnsAsIs();

		// Token: 0x0600002E RID: 46 RVA: 0x00002FD4 File Offset: 0x000011D4
		protected static Dictionary<string, ProviderPropertyDefinition> GetRequiredColumns(List<ColumnDefinition<int>> requiredColumnsIds, List<IColumnDefinition> requiredColumnsAsIs, string timeStampColumn = "Time")
		{
			Dictionary<string, ProviderPropertyDefinition> r = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
			{
				{
					timeStampColumn,
					null
				}
			};
			requiredColumnsIds.ForEach(delegate(ColumnDefinition<int> rc)
			{
				if (!r.ContainsKey(rc.ColumnName))
				{
					r.Add(rc.ColumnName, null);
				}
			});
			requiredColumnsAsIs.ForEach(delegate(IColumnDefinition rc)
			{
				if (!r.ContainsKey(rc.ColumnName))
				{
					r.Add(rc.ColumnName, null);
				}
			});
			return r;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000305C File Offset: 0x0000125C
		protected static Dictionary<string, ProviderPropertyDefinition> GetOptionalColumns(List<ColumnDefinition<int>> optionalColumnsIds, List<IColumnDefinition> optionalColumnsAsIs)
		{
			Dictionary<string, ProviderPropertyDefinition> o = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase);
			optionalColumnsIds.ForEach(delegate(ColumnDefinition<int> oc)
			{
				o.Add(oc.ColumnName, null);
			});
			optionalColumnsAsIs.ForEach(delegate(IColumnDefinition oc)
			{
				o.Add(oc.ColumnName, null);
			});
			return o;
		}

		// Token: 0x0400000B RID: 11
		public const string LoggingServerIdKey = "LoggingServerId";

		// Token: 0x0400000C RID: 12
		public const string TimestampColumn = "Time";

		// Token: 0x0400000D RID: 13
		public const int InternalMaximumRowCount = 2147483647;
	}
}
