using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000010 RID: 16
	internal class DatabaseInfoCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003B9A File Offset: 0x00001D9A
		public DatabaseInfoCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(DatabaseInfoCsvSchema.requiredColumnsIds, DatabaseInfoCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(DatabaseInfoCsvSchema.optionalColumnsIds, DatabaseInfoCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003BC6 File Offset: 0x00001DC6
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return DatabaseInfoCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003BCD File Offset: 0x00001DCD
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return DatabaseInfoCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return DatabaseInfoCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003BDB File Offset: 0x00001DDB
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return DatabaseInfoCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x04000032 RID: 50
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("DatabaseName", "DatabaseId", KnownStringType.DatabaseName)
		};

		// Token: 0x04000033 RID: 51
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<bool>("IsExcludedFromProvisioning"),
			new ColumnDefinition<bool>("IsExcludedFromProvisioningBySpaceMonitoring"),
			new ColumnDefinition<bool>("IsSuspendedFromProvisioning"),
			new ColumnDefinition<bool>("IsExcludedFromInitialProvisioning"),
			new ColumnDefinition<double>("DatabaseSizeInM"),
			new ColumnDefinition<double>("AvailableNewMailboxSpaceInM"),
			new ColumnDefinition<double>("ConnectedPhysicalSizeInM"),
			new ColumnDefinition<double>("ConnectedLogicalSizeInM"),
			new ColumnDefinition<double>("DisconnectedPhysicalSizeInM"),
			new ColumnDefinition<double>("DisconnectedLogicalSizeInM"),
			new ColumnDefinition<int>("ConnectedMbxCount"),
			new ColumnDefinition<int>("DisconnectedMbxCount"),
			new ColumnDefinition<double>("MoveDestinationPhysicalSizeInM"),
			new ColumnDefinition<double>("MoveDestinationLogicalSizeInM"),
			new ColumnDefinition<int>("MoveDestinationMbxCount"),
			new ColumnDefinition<double>("SoftDeletedPhysicalSizeInM"),
			new ColumnDefinition<double>("SoftDeletedLogicalSizeInM"),
			new ColumnDefinition<int>("SoftDeletedMbxCount")
		};

		// Token: 0x04000034 RID: 52
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x04000035 RID: 53
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<double>("DisconnectedIn48HoursMbxPhysicalSize"),
			new ColumnDefinition<int>("DisconnectedIn48HoursMbxCount")
		};
	}
}
