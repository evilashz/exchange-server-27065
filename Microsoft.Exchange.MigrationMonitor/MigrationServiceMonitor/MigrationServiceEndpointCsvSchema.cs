using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x02000008 RID: 8
	internal class MigrationServiceEndpointCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000360E File Offset: 0x0000180E
		public MigrationServiceEndpointCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MigrationServiceEndpointCsvSchema.requiredColumnsIds, MigrationServiceEndpointCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MigrationServiceEndpointCsvSchema.optionalColumnsIds, MigrationServiceEndpointCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000363A File Offset: 0x0000183A
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MigrationServiceEndpointCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003641 File Offset: 0x00001841
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MigrationServiceEndpointCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003648 File Offset: 0x00001848
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MigrationServiceEndpointCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000364F File Offset: 0x0000184F
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MigrationServiceEndpointCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x0400001D RID: 29
		public const string EndpointGuidColumn = "EndpointGuid";

		// Token: 0x0400001E RID: 30
		public const string EndpointNameColumn = "EndpointName";

		// Token: 0x0400001F RID: 31
		public const string EndpointRemoteColumn = "EndpointRemoteServer";

		// Token: 0x04000020 RID: 32
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x04000021 RID: 33
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("EndpointGuid")
		};

		// Token: 0x04000022 RID: 34
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("EndpointType", "EndpointTypeId", KnownStringType.MigrationType),
			new ColumnDefinition<int>("EndpointMailboxPermission", "EndpointMailboxPermissionId", KnownStringType.EndpointPermission),
			new ColumnDefinition<int>("EndpointState", "EndpointStateId", KnownStringType.EndpointState)
		};

		// Token: 0x04000023 RID: 35
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<string>("EndpointName"),
			new ColumnDefinition<string>("EndpointRemoteServer"),
			new ColumnDefinition<int>("EndpointMaxConcurrentMigrations"),
			new ColumnDefinition<int>("EndpointMaxConcurrentIncrementalSyncs"),
			new ColumnDefinition<SqlDateTime>("EndpointLastModifiedTime")
		};
	}
}
