using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000005 RID: 5
	public sealed class GlobalsTable
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002C10 File Offset: 0x00000E10
		internal GlobalsTable()
		{
			this.versionName = Factory.CreatePhysicalColumn("VersionName", "VersionName", typeof(string), false, false, false, false, false, Visibility.Public, 64, 0, 64);
			this.databaseVersion = Factory.CreatePhysicalColumn("DatabaseVersion", "DatabaseVersion", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.inid = Factory.CreatePhysicalColumn("Inid", "Inid", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastMaintenanceTask = Factory.CreatePhysicalColumn("LastMaintenanceTask", "LastMaintenanceTask", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.eventCounterLowerBound = Factory.CreatePhysicalColumn("EventCounterLowerBound", "EventCounterLowerBound", typeof(long), true, false, false, false, true, Visibility.Public, 0, 8, 8);
			this.eventCounterUpperBound = Factory.CreatePhysicalColumn("EventCounterUpperBound", "EventCounterUpperBound", typeof(long), true, false, false, false, true, Visibility.Public, 0, 8, 8);
			this.databaseDsGuid = Factory.CreatePhysicalColumn("DatabaseDsGuid", "DatabaseDsGuid", typeof(Guid), true, false, false, false, true, Visibility.Public, 0, 16, 16);
			string name = "GlobalsPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.globalsPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.VersionName
			});
			string name2 = "NewGlobalsPK";
			bool primaryKey2 = false;
			bool unique2 = true;
			bool schemaExtension2 = true;
			bool[] conditional2 = new bool[1];
			this.newGlobalsPK = new Index(name2, primaryKey2, unique2, schemaExtension2, conditional2, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.DatabaseVersion
			});
			Index[] indexes = new Index[]
			{
				this.GlobalsPK,
				this.NewGlobalsPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.VersionName,
				this.DatabaseVersion,
				this.Inid,
				this.LastMaintenanceTask,
				this.ExtensionBlob,
				this.EventCounterLowerBound,
				this.EventCounterUpperBound,
				this.DatabaseDsGuid
			};
			this.table = Factory.CreateTable("Globals", TableClass.Globals, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002E9B File Offset: 0x0000109B
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002EA3 File Offset: 0x000010A3
		public PhysicalColumn VersionName
		{
			get
			{
				return this.versionName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002EAB File Offset: 0x000010AB
		public PhysicalColumn DatabaseVersion
		{
			get
			{
				return this.databaseVersion;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002EB3 File Offset: 0x000010B3
		public PhysicalColumn Inid
		{
			get
			{
				return this.inid;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002EBB File Offset: 0x000010BB
		public PhysicalColumn LastMaintenanceTask
		{
			get
			{
				return this.lastMaintenanceTask;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002EC3 File Offset: 0x000010C3
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002ECB File Offset: 0x000010CB
		public PhysicalColumn EventCounterLowerBound
		{
			get
			{
				return this.eventCounterLowerBound;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002ED3 File Offset: 0x000010D3
		public PhysicalColumn EventCounterUpperBound
		{
			get
			{
				return this.eventCounterUpperBound;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002EDB File Offset: 0x000010DB
		public PhysicalColumn DatabaseDsGuid
		{
			get
			{
				return this.databaseDsGuid;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002EE3 File Offset: 0x000010E3
		public Index GlobalsPK
		{
			get
			{
				return this.globalsPK;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002EEB File Offset: 0x000010EB
		public Index NewGlobalsPK
		{
			get
			{
				return this.newGlobalsPK;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002EF4 File Offset: 0x000010F4
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.versionName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.versionName = null;
			}
			physicalColumn = this.databaseVersion;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.databaseVersion = null;
			}
			physicalColumn = this.inid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.inid = null;
			}
			physicalColumn = this.lastMaintenanceTask;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastMaintenanceTask = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.eventCounterLowerBound;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventCounterLowerBound = null;
			}
			physicalColumn = this.eventCounterUpperBound;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventCounterUpperBound = null;
			}
			physicalColumn = this.databaseDsGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.databaseDsGuid = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.globalsPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.globalsPK = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.newGlobalsPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.newGlobalsPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400001F RID: 31
		public const string VersionNameName = "VersionName";

		// Token: 0x04000020 RID: 32
		public const string DatabaseVersionName = "DatabaseVersion";

		// Token: 0x04000021 RID: 33
		public const string InidName = "Inid";

		// Token: 0x04000022 RID: 34
		public const string LastMaintenanceTaskName = "LastMaintenanceTask";

		// Token: 0x04000023 RID: 35
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000024 RID: 36
		public const string EventCounterLowerBoundName = "EventCounterLowerBound";

		// Token: 0x04000025 RID: 37
		public const string EventCounterUpperBoundName = "EventCounterUpperBound";

		// Token: 0x04000026 RID: 38
		public const string DatabaseDsGuidName = "DatabaseDsGuid";

		// Token: 0x04000027 RID: 39
		public const string PhysicalTableName = "Globals";

		// Token: 0x04000028 RID: 40
		private PhysicalColumn versionName;

		// Token: 0x04000029 RID: 41
		private PhysicalColumn databaseVersion;

		// Token: 0x0400002A RID: 42
		private PhysicalColumn inid;

		// Token: 0x0400002B RID: 43
		private PhysicalColumn lastMaintenanceTask;

		// Token: 0x0400002C RID: 44
		private PhysicalColumn extensionBlob;

		// Token: 0x0400002D RID: 45
		private PhysicalColumn eventCounterLowerBound;

		// Token: 0x0400002E RID: 46
		private PhysicalColumn eventCounterUpperBound;

		// Token: 0x0400002F RID: 47
		private PhysicalColumn databaseDsGuid;

		// Token: 0x04000030 RID: 48
		private Index globalsPK;

		// Token: 0x04000031 RID: 49
		private Index newGlobalsPK;

		// Token: 0x04000032 RID: 50
		private Table table;
	}
}
