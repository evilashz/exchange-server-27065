using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000011 RID: 17
	public sealed class ReceiveFolderTable
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00029898 File Offset: 0x00027A98
		internal ReceiveFolderTable()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageClass = Factory.CreatePhysicalColumn("MessageClass", "MessageClass", typeof(string), false, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "ReceiveFolderPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.receiveFolderPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.MessageClass
			});
			Index[] indexes = new Index[]
			{
				this.ReceiveFolderPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.MessageClass,
				this.FolderId,
				this.LastModificationTime,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("ReceiveFolder", TableClass.ReceiveFolder, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00029A58 File Offset: 0x00027C58
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00029A60 File Offset: 0x00027C60
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00029A68 File Offset: 0x00027C68
		public PhysicalColumn MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00029A70 File Offset: 0x00027C70
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00029A78 File Offset: 0x00027C78
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00029A80 File Offset: 0x00027C80
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00029A88 File Offset: 0x00027C88
		public Index ReceiveFolderPK
		{
			get
			{
				return this.receiveFolderPK;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00029A90 File Offset: 0x00027C90
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.messageClass;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageClass = null;
			}
			physicalColumn = this.folderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.folderId = null;
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
			Index index = this.receiveFolderPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.receiveFolderPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x040001AB RID: 427
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x040001AC RID: 428
		public const string MessageClassName = "MessageClass";

		// Token: 0x040001AD RID: 429
		public const string FolderIdName = "FolderId";

		// Token: 0x040001AE RID: 430
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x040001AF RID: 431
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040001B0 RID: 432
		public const string PhysicalTableName = "ReceiveFolder";

		// Token: 0x040001B1 RID: 433
		private PhysicalColumn mailboxNumber;

		// Token: 0x040001B2 RID: 434
		private PhysicalColumn messageClass;

		// Token: 0x040001B3 RID: 435
		private PhysicalColumn folderId;

		// Token: 0x040001B4 RID: 436
		private PhysicalColumn lastModificationTime;

		// Token: 0x040001B5 RID: 437
		private PhysicalColumn extensionBlob;

		// Token: 0x040001B6 RID: 438
		private Index receiveFolderPK;

		// Token: 0x040001B7 RID: 439
		private Table table;
	}
}
