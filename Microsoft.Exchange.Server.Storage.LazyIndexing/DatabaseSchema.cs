using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000008 RID: 8
	public class DatabaseSchema
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002D08 File Offset: 0x00000F08
		private DatabaseSchema(StoreDatabase database)
		{
			this.pseudoIndexDefinitionTable = new PseudoIndexDefinitionTable();
			database.PhysicalDatabase.AddTableMetadata(this.pseudoIndexDefinitionTable.Table);
			this.pseudoIndexControlTable = new PseudoIndexControlTable();
			database.PhysicalDatabase.AddTableMetadata(this.pseudoIndexControlTable.Table);
			this.pseudoIndexMaintenanceTable = new PseudoIndexMaintenanceTable();
			database.PhysicalDatabase.AddTableMetadata(this.pseudoIndexMaintenanceTable.Table);
			this.indexExplosionTableFunctionTableFunction = new IndexExplosionTableFunctionTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.indexExplosionTableFunctionTableFunction.TableFunction);
			this.columnMappingBlobTableFunction = new ColumnMappingBlobTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.columnMappingBlobTableFunction.TableFunction);
			this.conditionalIndexMappingBlobTableFunction = new ConditionalIndexMappingBlobTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.conditionalIndexMappingBlobTableFunction.TableFunction);
			this.indexDefinitionBlobTableFunction = new IndexDefinitionBlobTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.indexDefinitionBlobTableFunction.TableFunction);
			this.columnMappingBlobHeaderTableFunction = new ColumnMappingBlobHeaderTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.columnMappingBlobHeaderTableFunction.TableFunction);
			this.indexDefinitionBlobHeaderTableFunction = new IndexDefinitionBlobHeaderTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.indexDefinitionBlobHeaderTableFunction.TableFunction);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E44 File Offset: 0x00001044
		internal static void Initialize()
		{
			if (DatabaseSchema.databaseSchemaDataSlot == -1)
			{
				DatabaseSchema.databaseSchemaDataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E58 File Offset: 0x00001058
		internal static void Initialize(StoreDatabase database)
		{
			database.ComponentData[DatabaseSchema.databaseSchemaDataSlot] = new DatabaseSchema(database);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E70 File Offset: 0x00001070
		internal static void PostMountInitialize(Context context, StoreDatabase database)
		{
			if (database.PhysicalDatabase.DatabaseType != DatabaseType.Jet)
			{
				return;
			}
			ComponentVersion currentSchemaVersion = database.GetCurrentSchemaVersion(context);
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			Table table = databaseSchema.pseudoIndexDefinitionTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.pseudoIndexDefinitionTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.pseudoIndexDefinitionTable = null;
			}
			table = databaseSchema.pseudoIndexControlTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.pseudoIndexControlTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.pseudoIndexControlTable = null;
			}
			table = databaseSchema.pseudoIndexMaintenanceTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.pseudoIndexMaintenanceTable.PostMountInitialize(currentSchemaVersion);
				return;
			}
			database.PhysicalDatabase.RemoveTableMetadata(table.Name);
			databaseSchema.pseudoIndexMaintenanceTable = null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F98 File Offset: 0x00001198
		public static PseudoIndexDefinitionTable PseudoIndexDefinitionTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.pseudoIndexDefinitionTable;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FC4 File Offset: 0x000011C4
		public static PseudoIndexControlTable PseudoIndexControlTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.pseudoIndexControlTable;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static PseudoIndexMaintenanceTable PseudoIndexMaintenanceTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.pseudoIndexMaintenanceTable;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000301C File Offset: 0x0000121C
		public static IndexExplosionTableFunctionTableFunction IndexExplosionTableFunctionTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.indexExplosionTableFunctionTableFunction;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003048 File Offset: 0x00001248
		public static ColumnMappingBlobTableFunction ColumnMappingBlobTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.columnMappingBlobTableFunction;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003074 File Offset: 0x00001274
		public static ConditionalIndexMappingBlobTableFunction ConditionalIndexMappingBlobTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.conditionalIndexMappingBlobTableFunction;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000030A0 File Offset: 0x000012A0
		public static IndexDefinitionBlobTableFunction IndexDefinitionBlobTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.indexDefinitionBlobTableFunction;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030CC File Offset: 0x000012CC
		public static ColumnMappingBlobHeaderTableFunction ColumnMappingBlobHeaderTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.columnMappingBlobHeaderTableFunction;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000030F8 File Offset: 0x000012F8
		public static IndexDefinitionBlobHeaderTableFunction IndexDefinitionBlobHeaderTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.indexDefinitionBlobHeaderTableFunction;
		}

		// Token: 0x04000021 RID: 33
		private static int databaseSchemaDataSlot = -1;

		// Token: 0x04000022 RID: 34
		private PseudoIndexDefinitionTable pseudoIndexDefinitionTable;

		// Token: 0x04000023 RID: 35
		private PseudoIndexControlTable pseudoIndexControlTable;

		// Token: 0x04000024 RID: 36
		private PseudoIndexMaintenanceTable pseudoIndexMaintenanceTable;

		// Token: 0x04000025 RID: 37
		private IndexExplosionTableFunctionTableFunction indexExplosionTableFunctionTableFunction;

		// Token: 0x04000026 RID: 38
		private ColumnMappingBlobTableFunction columnMappingBlobTableFunction;

		// Token: 0x04000027 RID: 39
		private ConditionalIndexMappingBlobTableFunction conditionalIndexMappingBlobTableFunction;

		// Token: 0x04000028 RID: 40
		private IndexDefinitionBlobTableFunction indexDefinitionBlobTableFunction;

		// Token: 0x04000029 RID: 41
		private ColumnMappingBlobHeaderTableFunction columnMappingBlobHeaderTableFunction;

		// Token: 0x0400002A RID: 42
		private IndexDefinitionBlobHeaderTableFunction indexDefinitionBlobHeaderTableFunction;
	}
}
