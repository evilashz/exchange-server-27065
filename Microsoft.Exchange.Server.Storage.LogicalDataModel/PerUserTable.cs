using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200000F RID: 15
	public sealed class PerUserTable
	{
		// Token: 0x06000107 RID: 263 RVA: 0x0000A074 File Offset: 0x00008274
		internal PerUserTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.residentFolder = Factory.CreatePhysicalColumn("ResidentFolder", "ResidentFolder", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.guid = Factory.CreatePhysicalColumn("Guid", "Guid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 26, 0, 26);
			this.cnsetRead = Factory.CreatePhysicalColumn("CnsetRead", "CnsetRead", typeof(byte[]), false, false, false, false, false, Visibility.Public, 1048576, 0, 1048576);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "PerUserPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[4];
			this.perUserPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.ResidentFolder,
				this.FolderId,
				this.Guid
			});
			Index[] indexes = new Index[]
			{
				this.PerUserPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.ResidentFolder,
				this.Guid,
				this.FolderId,
				this.CnsetRead,
				this.LastModificationTime,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("PerUser", TableClass.PerUser, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000A2AB File Offset: 0x000084AB
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000A2B3 File Offset: 0x000084B3
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000A2BB File Offset: 0x000084BB
		public PhysicalColumn ResidentFolder
		{
			get
			{
				return this.residentFolder;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000A2C3 File Offset: 0x000084C3
		public PhysicalColumn Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000A2CB File Offset: 0x000084CB
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000A2D3 File Offset: 0x000084D3
		public PhysicalColumn CnsetRead
		{
			get
			{
				return this.cnsetRead;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000A2DB File Offset: 0x000084DB
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000A2E3 File Offset: 0x000084E3
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000A2EB File Offset: 0x000084EB
		public Index PerUserPK
		{
			get
			{
				return this.perUserPK;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A2F4 File Offset: 0x000084F4
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.residentFolder;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.residentFolder = null;
			}
			physicalColumn = this.guid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.guid = null;
			}
			physicalColumn = this.folderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.folderId = null;
			}
			physicalColumn = this.cnsetRead;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.cnsetRead = null;
			}
			physicalColumn = this.lastModificationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastModificationTime = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.perUserPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.perUserPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400019A RID: 410
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400019B RID: 411
		public const string ResidentFolderName = "ResidentFolder";

		// Token: 0x0400019C RID: 412
		public const string GuidName = "Guid";

		// Token: 0x0400019D RID: 413
		public const string FolderIdName = "FolderId";

		// Token: 0x0400019E RID: 414
		public const string CnsetReadName = "CnsetRead";

		// Token: 0x0400019F RID: 415
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x040001A0 RID: 416
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040001A1 RID: 417
		public const string PhysicalTableName = "PerUser";

		// Token: 0x040001A2 RID: 418
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x040001A3 RID: 419
		private PhysicalColumn residentFolder;

		// Token: 0x040001A4 RID: 420
		private PhysicalColumn guid;

		// Token: 0x040001A5 RID: 421
		private PhysicalColumn folderId;

		// Token: 0x040001A6 RID: 422
		private PhysicalColumn cnsetRead;

		// Token: 0x040001A7 RID: 423
		private PhysicalColumn lastModificationTime;

		// Token: 0x040001A8 RID: 424
		private PhysicalColumn extensionBlob;

		// Token: 0x040001A9 RID: 425
		private Index perUserPK;

		// Token: 0x040001AA RID: 426
		private Table table;
	}
}
