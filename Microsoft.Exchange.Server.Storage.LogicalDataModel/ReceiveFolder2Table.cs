using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000012 RID: 18
	public sealed class ReceiveFolder2Table
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00029CA0 File Offset: 0x00027EA0
		internal ReceiveFolder2Table()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageClass = Factory.CreatePhysicalColumn("MessageClass", "MessageClass", typeof(byte[]), false, false, false, false, false, Visibility.Public, 255, 0, 255);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "ReceiveFolder2PK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.receiveFolder2PK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
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
				this.ReceiveFolder2PK
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
			this.table = Factory.CreateTable("ReceiveFolder2", TableClass.ReceiveFolder2, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, true, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00029E60 File Offset: 0x00028060
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00029E68 File Offset: 0x00028068
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00029E70 File Offset: 0x00028070
		public PhysicalColumn MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00029E78 File Offset: 0x00028078
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00029E80 File Offset: 0x00028080
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00029E88 File Offset: 0x00028088
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00029E90 File Offset: 0x00028090
		public Index ReceiveFolder2PK
		{
			get
			{
				return this.receiveFolder2PK;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00029E98 File Offset: 0x00028098
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
			Index index = this.receiveFolder2PK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.receiveFolder2PK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x040001B8 RID: 440
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x040001B9 RID: 441
		public const string MessageClassName = "MessageClass";

		// Token: 0x040001BA RID: 442
		public const string FolderIdName = "FolderId";

		// Token: 0x040001BB RID: 443
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x040001BC RID: 444
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040001BD RID: 445
		public const string PhysicalTableName = "ReceiveFolder2";

		// Token: 0x040001BE RID: 446
		private PhysicalColumn mailboxNumber;

		// Token: 0x040001BF RID: 447
		private PhysicalColumn messageClass;

		// Token: 0x040001C0 RID: 448
		private PhysicalColumn folderId;

		// Token: 0x040001C1 RID: 449
		private PhysicalColumn lastModificationTime;

		// Token: 0x040001C2 RID: 450
		private PhysicalColumn extensionBlob;

		// Token: 0x040001C3 RID: 451
		private Index receiveFolder2PK;

		// Token: 0x040001C4 RID: 452
		private Table table;
	}
}
