using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000002 RID: 2
	public class DatabaseSchema
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private DatabaseSchema(StoreDatabase database)
		{
			this.extendedPropertyNameMappingTable = new ExtendedPropertyNameMappingTable();
			database.PhysicalDatabase.AddTableMetadata(this.extendedPropertyNameMappingTable.Table);
			this.replidGuidMapTable = new ReplidGuidMapTable();
			database.PhysicalDatabase.AddTableMetadata(this.replidGuidMapTable.Table);
			this.mailboxIdentityTable = new MailboxIdentityTable();
			database.PhysicalDatabase.AddTableMetadata(this.mailboxIdentityTable.Table);
			this.mailboxTable = new MailboxTable();
			database.PhysicalDatabase.AddTableMetadata(this.mailboxTable.Table);
			this.globalsTable = new GlobalsTable();
			database.PhysicalDatabase.AddTableMetadata(this.globalsTable.Table);
			this.upgradeHistoryTable = new UpgradeHistoryTable();
			database.PhysicalDatabase.AddTableMetadata(this.upgradeHistoryTable.Table);
			this.timedEventsTable = new TimedEventsTable();
			database.PhysicalDatabase.AddTableMetadata(this.timedEventsTable.Table);
			this.fullTextIndexTableFunctionTableFunction = new FullTextIndexTableFunctionTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.fullTextIndexTableFunctionTableFunction.TableFunction);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021EB File Offset: 0x000003EB
		internal static void Initialize()
		{
			if (DatabaseSchema.databaseSchemaDataSlot == -1)
			{
				DatabaseSchema.databaseSchemaDataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021FF File Offset: 0x000003FF
		internal static void Initialize(StoreDatabase database)
		{
			database.ComponentData[DatabaseSchema.databaseSchemaDataSlot] = new DatabaseSchema(database);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002218 File Offset: 0x00000418
		internal static void PostMountInitialize(Context context, StoreDatabase database)
		{
			if (database.PhysicalDatabase.DatabaseType != DatabaseType.Jet)
			{
				return;
			}
			ComponentVersion currentSchemaVersion = database.GetCurrentSchemaVersion(context);
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			Table table = databaseSchema.extendedPropertyNameMappingTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.extendedPropertyNameMappingTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.extendedPropertyNameMappingTable = null;
			}
			table = databaseSchema.replidGuidMapTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.replidGuidMapTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.replidGuidMapTable = null;
			}
			table = databaseSchema.mailboxIdentityTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.mailboxIdentityTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.mailboxIdentityTable = null;
			}
			table = databaseSchema.mailboxTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.mailboxTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.mailboxTable = null;
			}
			table = databaseSchema.globalsTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.globalsTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.globalsTable = null;
			}
			table = databaseSchema.upgradeHistoryTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.upgradeHistoryTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.upgradeHistoryTable = null;
			}
			table = databaseSchema.timedEventsTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.timedEventsTable.PostMountInitialize(currentSchemaVersion);
				return;
			}
			database.PhysicalDatabase.RemoveTableMetadata(table.Name);
			databaseSchema.timedEventsTable = null;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002480 File Offset: 0x00000680
		public static ExtendedPropertyNameMappingTable ExtendedPropertyNameMappingTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.extendedPropertyNameMappingTable;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000024AC File Offset: 0x000006AC
		public static ReplidGuidMapTable ReplidGuidMapTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.replidGuidMapTable;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000024D8 File Offset: 0x000006D8
		public static MailboxIdentityTable MailboxIdentityTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.mailboxIdentityTable;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002504 File Offset: 0x00000704
		public static MailboxTable MailboxTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.mailboxTable;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002530 File Offset: 0x00000730
		public static GlobalsTable GlobalsTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.globalsTable;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000255C File Offset: 0x0000075C
		public static UpgradeHistoryTable UpgradeHistoryTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.upgradeHistoryTable;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002588 File Offset: 0x00000788
		public static TimedEventsTable TimedEventsTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.timedEventsTable;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025B4 File Offset: 0x000007B4
		public static FullTextIndexTableFunctionTableFunction FullTextIndexTableFunctionTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.fullTextIndexTableFunctionTableFunction;
		}

		// Token: 0x04000001 RID: 1
		private static int databaseSchemaDataSlot = -1;

		// Token: 0x04000002 RID: 2
		private ExtendedPropertyNameMappingTable extendedPropertyNameMappingTable;

		// Token: 0x04000003 RID: 3
		private ReplidGuidMapTable replidGuidMapTable;

		// Token: 0x04000004 RID: 4
		private MailboxIdentityTable mailboxIdentityTable;

		// Token: 0x04000005 RID: 5
		private MailboxTable mailboxTable;

		// Token: 0x04000006 RID: 6
		private GlobalsTable globalsTable;

		// Token: 0x04000007 RID: 7
		private UpgradeHistoryTable upgradeHistoryTable;

		// Token: 0x04000008 RID: 8
		private TimedEventsTable timedEventsTable;

		// Token: 0x04000009 RID: 9
		private FullTextIndexTableFunctionTableFunction fullTextIndexTableFunctionTableFunction;
	}
}
