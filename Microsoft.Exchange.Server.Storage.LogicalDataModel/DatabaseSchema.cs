using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000005 RID: 5
	public class DatabaseSchema
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002720 File Offset: 0x00000920
		private DatabaseSchema(StoreDatabase database)
		{
			this.deliveredToTable = new DeliveredToTable();
			database.PhysicalDatabase.AddTableMetadata(this.deliveredToTable.Table);
			this.eventsTable = new EventsTable();
			database.PhysicalDatabase.AddTableMetadata(this.eventsTable.Table);
			this.attachmentTable = new AttachmentTable();
			database.PhysicalDatabase.AddTableMetadata(this.attachmentTable.Table);
			this.folderTable = new FolderTable();
			database.PhysicalDatabase.AddTableMetadata(this.folderTable.Table);
			this.inferenceLogTable = new InferenceLogTable();
			database.PhysicalDatabase.AddTableMetadata(this.inferenceLogTable.Table);
			this.messageTable = new MessageTable();
			database.PhysicalDatabase.AddTableMetadata(this.messageTable.Table);
			this.perUserTable = new PerUserTable();
			database.PhysicalDatabase.AddTableMetadata(this.perUserTable.Table);
			this.receiveFolderTable = new ReceiveFolderTable();
			database.PhysicalDatabase.AddTableMetadata(this.receiveFolderTable.Table);
			this.receiveFolder2Table = new ReceiveFolder2Table();
			database.PhysicalDatabase.AddTableMetadata(this.receiveFolder2Table.Table);
			this.searchQueueTable = new SearchQueueTable();
			database.PhysicalDatabase.AddTableMetadata(this.searchQueueTable.Table);
			this.tombstoneTable = new TombstoneTable();
			database.PhysicalDatabase.AddTableMetadata(this.tombstoneTable.Table);
			this.watermarksTable = new WatermarksTable();
			database.PhysicalDatabase.AddTableMetadata(this.watermarksTable.Table);
			this.userInfoTable = new UserInfoTable();
			database.PhysicalDatabase.AddTableMetadata(this.userInfoTable.Table);
			this.attachmentTableFunctionTableFunction = new AttachmentTableFunctionTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.attachmentTableFunctionTableFunction.TableFunction);
			this.recipientTableFunctionTableFunction = new RecipientTableFunctionTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.recipientTableFunctionTableFunction.TableFunction);
			this.conversationMembersBlobTableFunction = new ConversationMembersBlobTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.conversationMembersBlobTableFunction.TableFunction);
			this.folderHierarchyBlobTableFunction = new FolderHierarchyBlobTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.folderHierarchyBlobTableFunction.TableFunction);
			this.searchResultsTableFunction = new SearchResultsTableFunction();
			database.PhysicalDatabase.AddTableMetadata(this.searchResultsTableFunction.TableFunction);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002985 File Offset: 0x00000B85
		internal static void Initialize()
		{
			if (DatabaseSchema.databaseSchemaDataSlot == -1)
			{
				DatabaseSchema.databaseSchemaDataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000299C File Offset: 0x00000B9C
		internal static void MountEventHandlerForFullTextIndex(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			databaseSchema.messageTable.SetupFullTextIndex(database);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029CB File Offset: 0x00000BCB
		internal static void Initialize(StoreDatabase database)
		{
			database.ComponentData[DatabaseSchema.databaseSchemaDataSlot] = new DatabaseSchema(database);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal static void PostMountInitialize(Context context, StoreDatabase database)
		{
			if (database.PhysicalDatabase.DatabaseType != DatabaseType.Jet)
			{
				return;
			}
			ComponentVersion currentSchemaVersion = database.GetCurrentSchemaVersion(context);
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			Table table = databaseSchema.deliveredToTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.deliveredToTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.deliveredToTable = null;
			}
			table = databaseSchema.eventsTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.eventsTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.eventsTable = null;
			}
			table = databaseSchema.attachmentTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.attachmentTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.attachmentTable = null;
			}
			table = databaseSchema.folderTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.folderTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.folderTable = null;
			}
			table = databaseSchema.inferenceLogTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.inferenceLogTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.inferenceLogTable = null;
			}
			table = databaseSchema.messageTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.messageTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.messageTable = null;
			}
			table = databaseSchema.perUserTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.perUserTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.perUserTable = null;
			}
			table = databaseSchema.receiveFolderTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.receiveFolderTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.receiveFolderTable = null;
			}
			table = databaseSchema.receiveFolder2Table.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.receiveFolder2Table.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.receiveFolder2Table = null;
			}
			table = databaseSchema.searchQueueTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.searchQueueTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.searchQueueTable = null;
			}
			table = databaseSchema.tombstoneTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.tombstoneTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.tombstoneTable = null;
			}
			table = databaseSchema.watermarksTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.watermarksTable.PostMountInitialize(currentSchemaVersion);
			}
			else
			{
				database.PhysicalDatabase.RemoveTableMetadata(table.Name);
				databaseSchema.watermarksTable = null;
			}
			table = databaseSchema.userInfoTable.Table;
			if (table.MinVersion <= currentSchemaVersion.Value && currentSchemaVersion.Value <= table.MaxVersion)
			{
				databaseSchema.userInfoTable.PostMountInitialize(currentSchemaVersion);
				return;
			}
			database.PhysicalDatabase.RemoveTableMetadata(table.Name);
			databaseSchema.userInfoTable = null;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E2C File Offset: 0x0000102C
		public static DeliveredToTable DeliveredToTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.deliveredToTable;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E58 File Offset: 0x00001058
		public static EventsTable EventsTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.eventsTable;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E84 File Offset: 0x00001084
		public static AttachmentTable AttachmentTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.attachmentTable;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static FolderTable FolderTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.folderTable;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002EDC File Offset: 0x000010DC
		public static InferenceLogTable InferenceLogTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.inferenceLogTable;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002F08 File Offset: 0x00001108
		public static MessageTable MessageTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.messageTable;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002F34 File Offset: 0x00001134
		public static PerUserTable PerUserTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.perUserTable;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F60 File Offset: 0x00001160
		public static ReceiveFolderTable ReceiveFolderTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.receiveFolderTable;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F8C File Offset: 0x0000118C
		public static ReceiveFolder2Table ReceiveFolder2Table(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.receiveFolder2Table;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static SearchQueueTable SearchQueueTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.searchQueueTable;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static TombstoneTable TombstoneTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.tombstoneTable;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003010 File Offset: 0x00001210
		public static WatermarksTable WatermarksTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.watermarksTable;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000303C File Offset: 0x0000123C
		public static UserInfoTable UserInfoTable(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.userInfoTable;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003068 File Offset: 0x00001268
		public static AttachmentTableFunctionTableFunction AttachmentTableFunctionTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.attachmentTableFunctionTableFunction;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003094 File Offset: 0x00001294
		public static RecipientTableFunctionTableFunction RecipientTableFunctionTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.recipientTableFunctionTableFunction;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000030C0 File Offset: 0x000012C0
		public static ConversationMembersBlobTableFunction ConversationMembersBlobTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.conversationMembersBlobTableFunction;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030EC File Offset: 0x000012EC
		public static FolderHierarchyBlobTableFunction FolderHierarchyBlobTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.folderHierarchyBlobTableFunction;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003118 File Offset: 0x00001318
		public static SearchResultsTableFunction SearchResultsTableFunction(StoreDatabase database)
		{
			DatabaseSchema databaseSchema = (DatabaseSchema)database.ComponentData[DatabaseSchema.databaseSchemaDataSlot];
			return databaseSchema.searchResultsTableFunction;
		}

		// Token: 0x04000013 RID: 19
		private static int databaseSchemaDataSlot = -1;

		// Token: 0x04000014 RID: 20
		private DeliveredToTable deliveredToTable;

		// Token: 0x04000015 RID: 21
		private EventsTable eventsTable;

		// Token: 0x04000016 RID: 22
		private AttachmentTable attachmentTable;

		// Token: 0x04000017 RID: 23
		private FolderTable folderTable;

		// Token: 0x04000018 RID: 24
		private InferenceLogTable inferenceLogTable;

		// Token: 0x04000019 RID: 25
		private MessageTable messageTable;

		// Token: 0x0400001A RID: 26
		private PerUserTable perUserTable;

		// Token: 0x0400001B RID: 27
		private ReceiveFolderTable receiveFolderTable;

		// Token: 0x0400001C RID: 28
		private ReceiveFolder2Table receiveFolder2Table;

		// Token: 0x0400001D RID: 29
		private SearchQueueTable searchQueueTable;

		// Token: 0x0400001E RID: 30
		private TombstoneTable tombstoneTable;

		// Token: 0x0400001F RID: 31
		private WatermarksTable watermarksTable;

		// Token: 0x04000020 RID: 32
		private UserInfoTable userInfoTable;

		// Token: 0x04000021 RID: 33
		private AttachmentTableFunctionTableFunction attachmentTableFunctionTableFunction;

		// Token: 0x04000022 RID: 34
		private RecipientTableFunctionTableFunction recipientTableFunctionTableFunction;

		// Token: 0x04000023 RID: 35
		private ConversationMembersBlobTableFunction conversationMembersBlobTableFunction;

		// Token: 0x04000024 RID: 36
		private FolderHierarchyBlobTableFunction folderHierarchyBlobTableFunction;

		// Token: 0x04000025 RID: 37
		private SearchResultsTableFunction searchResultsTableFunction;
	}
}
