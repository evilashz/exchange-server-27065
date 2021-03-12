using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200000D RID: 13
	public sealed class ReplidGuidMapTable
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000B5B0 File Offset: 0x000097B0
		internal ReplidGuidMapTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.guid = Factory.CreatePhysicalColumn("Guid", "Guid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.replid = Factory.CreatePhysicalColumn("Replid", "Replid", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "ReplidGuidMapPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.replidGuidMapPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.Guid
			});
			Index[] indexes = new Index[]
			{
				this.ReplidGuidMapPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.Guid,
				this.Replid,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("ReplidGuidMap", TableClass.ReplidGuidMap, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000B736 File Offset: 0x00009936
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000B73E File Offset: 0x0000993E
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000B746 File Offset: 0x00009946
		public PhysicalColumn Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000B74E File Offset: 0x0000994E
		public PhysicalColumn Replid
		{
			get
			{
				return this.replid;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000B756 File Offset: 0x00009956
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000B75E File Offset: 0x0000995E
		public Index ReplidGuidMapPK
		{
			get
			{
				return this.replidGuidMapPK;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000B768 File Offset: 0x00009968
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.guid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.guid = null;
			}
			physicalColumn = this.replid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.replid = null;
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
			Index index = this.replidGuidMapPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.replidGuidMapPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400014F RID: 335
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x04000150 RID: 336
		public const string GuidName = "Guid";

		// Token: 0x04000151 RID: 337
		public const string ReplidName = "Replid";

		// Token: 0x04000152 RID: 338
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000153 RID: 339
		public const string PhysicalTableName = "ReplidGuidMap";

		// Token: 0x04000154 RID: 340
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000155 RID: 341
		private PhysicalColumn guid;

		// Token: 0x04000156 RID: 342
		private PhysicalColumn replid;

		// Token: 0x04000157 RID: 343
		private PhysicalColumn extensionBlob;

		// Token: 0x04000158 RID: 344
		private Index replidGuidMapPK;

		// Token: 0x04000159 RID: 345
		private Table table;
	}
}
