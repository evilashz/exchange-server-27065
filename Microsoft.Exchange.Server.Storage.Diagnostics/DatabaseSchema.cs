using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000003 RID: 3
	public class DatabaseSchema
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000026E4 File Offset: 0x000008E4
		private DatabaseSchema(StoreDatabase database)
		{
			this.mSysObjectsTable = new MSysObjectsTable();
			database.PhysicalDatabase.AddTableMetadata(this.mSysObjectsTable.Table);
			this.mSysObjidsTable = new MSysObjidsTable();
			database.PhysicalDatabase.AddTableMetadata(this.mSysObjidsTable.Table);
			this.catalogTableFunction = new CatalogTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.catalogTableFunction.TableFunction);
			this.spaceUsageTableFunction = new SpaceUsageTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.spaceUsageTableFunction.TableFunction);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000277B File Offset: 0x0000097B
		internal static void Initialize()
		{
			if (DatabaseSchema.databaseSchemaDataSlot == -1)
			{
				DatabaseSchema.databaseSchemaDataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000278F File Offset: 0x0000098F
		internal static void Initialize(StoreDatabase database)
		{
			database.ComponentData[DatabaseSchema.databaseSchemaDataSlot] = new DatabaseSchema(database);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027A8 File Offset: 0x000009A8
		internal static void PostMountInitialize(Context context, StoreDatabase database)
		{
			if (database.PhysicalDatabase.DatabaseType != DatabaseType.Jet)
			{
				return;
			}
			ComponentVersion currentSchemaVersion = database.GetCurrentSchemaVersion(context);
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			Table table = databaseSchema.mSysObjectsTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.mSysObjectsTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.mSysObjectsTable = null;
			}
			table = databaseSchema.mSysObjidsTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.mSysObjidsTable.PostMountInitialize(currentSchemaVersion);
				return;
			}
			database.PhysicalDatabase.RemoveTableMetadata(table.Name);
			databaseSchema.mSysObjidsTable = null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002880 File Offset: 0x00000A80
		public static MSysObjectsTable MSysObjectsTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.mSysObjectsTable;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028AC File Offset: 0x00000AAC
		public static MSysObjidsTable MSysObjidsTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.mSysObjidsTable;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static CatalogTableFunction CatalogTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.catalogTableFunction;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002904 File Offset: 0x00000B04
		public static SpaceUsageTableFunction SpaceUsageTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.spaceUsageTableFunction;
		}

		// Token: 0x04000010 RID: 16
		private static int databaseSchemaDataSlot = -1;

		// Token: 0x04000011 RID: 17
		private MSysObjectsTable mSysObjectsTable;

		// Token: 0x04000012 RID: 18
		private MSysObjidsTable mSysObjidsTable;

		// Token: 0x04000013 RID: 19
		private CatalogTableFunction catalogTableFunction;

		// Token: 0x04000014 RID: 20
		private SpaceUsageTableFunction spaceUsageTableFunction;
	}
}
