using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000016 RID: 22
	public sealed class TombstoneTable
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0002A9FC File Offset: 0x00028BFC
		internal TombstoneTable()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.inid = Factory.CreatePhysicalColumn("Inid", "Inid", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.sizeEstimate = Factory.CreatePhysicalColumn("SizeEstimate", "SizeEstimate", typeof(long), true, false, false, false, true, Visibility.Public, 0, 8, 8);
			this.clientType = Factory.CreatePhysicalColumn("ClientType", "ClientType", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			string name = "TombstonePK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.tombstonePK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.Inid
			});
			Index[] indexes = new Index[]
			{
				this.TombstonePK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.Inid,
				this.ExtensionBlob,
				this.SizeEstimate,
				this.ClientType
			};
			this.table = Factory.CreateTable("Tombstone", TableClass.Tombstone, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0002ABB2 File Offset: 0x00028DB2
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0002ABBA File Offset: 0x00028DBA
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0002ABC2 File Offset: 0x00028DC2
		public PhysicalColumn Inid
		{
			get
			{
				return this.inid;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0002ABCA File Offset: 0x00028DCA
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0002ABD2 File Offset: 0x00028DD2
		public PhysicalColumn SizeEstimate
		{
			get
			{
				return this.sizeEstimate;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0002ABDA File Offset: 0x00028DDA
		public PhysicalColumn ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0002ABE2 File Offset: 0x00028DE2
		public Index TombstonePK
		{
			get
			{
				return this.tombstonePK;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0002ABEC File Offset: 0x00028DEC
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.inid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.inid = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.sizeEstimate;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.sizeEstimate = null;
			}
			physicalColumn = this.clientType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.clientType = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.tombstonePK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.tombstonePK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x040001E8 RID: 488
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x040001E9 RID: 489
		public const string InidName = "Inid";

		// Token: 0x040001EA RID: 490
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040001EB RID: 491
		public const string SizeEstimateName = "SizeEstimate";

		// Token: 0x040001EC RID: 492
		public const string ClientTypeName = "ClientType";

		// Token: 0x040001ED RID: 493
		public const string PhysicalTableName = "Tombstone";

		// Token: 0x040001EE RID: 494
		private PhysicalColumn mailboxNumber;

		// Token: 0x040001EF RID: 495
		private PhysicalColumn inid;

		// Token: 0x040001F0 RID: 496
		private PhysicalColumn extensionBlob;

		// Token: 0x040001F1 RID: 497
		private PhysicalColumn sizeEstimate;

		// Token: 0x040001F2 RID: 498
		private PhysicalColumn clientType;

		// Token: 0x040001F3 RID: 499
		private Index tombstonePK;

		// Token: 0x040001F4 RID: 500
		private Table table;
	}
}
