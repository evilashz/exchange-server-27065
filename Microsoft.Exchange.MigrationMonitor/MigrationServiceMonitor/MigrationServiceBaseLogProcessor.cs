using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x02000003 RID: 3
	internal abstract class MigrationServiceBaseLogProcessor : BaseLogProcessor
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002CC4 File Offset: 0x00000EC4
		protected MigrationServiceBaseLogProcessor(string logDirectoryPath, string logFileTypeName, BaseMigMonCsvSchema csvSchemaInstance) : base(logDirectoryPath, logFileTypeName, csvSchemaInstance, null)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CD0 File Offset: 0x00000ED0
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			return this.TryAddMigrationServiceCommonDataRowValues(dataRow, row);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002CDC File Offset: 0x00000EDC
		protected bool TryAddMigrationServiceCommonDataRowValues(DataRow dataRow, CsvRow row)
		{
			base.TryAddSimpleOptionalKnownStrings(dataRow, row);
			dataRow["InternalError"] = DBNull.Value;
			dataRow["LocalizedError"] = DBNull.Value;
			dataRow[MigrationServiceProcessorsCommonHelpers.TenantName.DataTableKeyColumnName] = DBNull.Value;
			dataRow[MigrationServiceProcessorsCommonHelpers.WatsonHash.DataTableKeyColumnName] = DBNull.Value;
			string columnStringValue = MigMonUtilities.GetColumnStringValue(row, "LocalizedError");
			string columnStringValue2 = MigMonUtilities.GetColumnStringValue(row, "InternalError");
			string columnStringValue3 = MigMonUtilities.GetColumnStringValue(row, MigrationServiceProcessorsCommonHelpers.WatsonHash.ColumnName);
			if (!string.IsNullOrWhiteSpace(columnStringValue3))
			{
				dataRow[MigrationServiceProcessorsCommonHelpers.WatsonHash.DataTableKeyColumnName] = MigMonUtilities.GetWatsonHashId(columnStringValue3, columnStringValue + Environment.NewLine + columnStringValue2, "MigrationService");
			}
			string columnStringValue4 = MigMonUtilities.GetColumnStringValue(row, MigrationServiceProcessorsCommonHelpers.TenantName.ColumnName);
			if (!string.IsNullOrWhiteSpace(columnStringValue4))
			{
				dataRow[MigrationServiceProcessorsCommonHelpers.TenantName.DataTableKeyColumnName] = MigMonUtilities.GetTenantNameId(columnStringValue4);
			}
			return true;
		}

		// Token: 0x0400000A RID: 10
		public new const string KeyNameIsLogProcessorEnabled = "IsMigServiceStatsLogProcessorEnabled";
	}
}
