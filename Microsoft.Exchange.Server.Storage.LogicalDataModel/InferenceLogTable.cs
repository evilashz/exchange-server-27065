using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200000C RID: 12
	public sealed class InferenceLogTable
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00007960 File Offset: 0x00005B60
		internal InferenceLogTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.rowId = Factory.CreatePhysicalColumn("RowId", "RowId", typeof(int), false, true, false, false, false, Visibility.Public, 0, 4, 4);
			this.createTime = Factory.CreatePhysicalColumn("CreateTime", "CreateTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.propertyBlob = Factory.CreatePhysicalColumn("PropertyBlob", "PropertyBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 4096, 0, 4096);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			string name = "InferenceLogPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.inferenceLogPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.RowId
			});
			Index[] indexes = new Index[]
			{
				this.InferenceLogPK
			};
			SpecialColumns specialCols = new SpecialColumns(this.PropertyBlob, null, null, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.RowId,
				this.CreateTime,
				this.PropertyBlob,
				this.MailboxNumber
			};
			this.table = Factory.CreateTable("InferenceLog", TableClass.InferenceLog, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Redacted, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00007B1B File Offset: 0x00005D1B
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007B23 File Offset: 0x00005D23
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00007B2B File Offset: 0x00005D2B
		public PhysicalColumn RowId
		{
			get
			{
				return this.rowId;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00007B33 File Offset: 0x00005D33
		public PhysicalColumn CreateTime
		{
			get
			{
				return this.createTime;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00007B3B File Offset: 0x00005D3B
		public PhysicalColumn PropertyBlob
		{
			get
			{
				return this.propertyBlob;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00007B43 File Offset: 0x00005D43
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007B4B File Offset: 0x00005D4B
		public Index InferenceLogPK
		{
			get
			{
				return this.inferenceLogPK;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007B54 File Offset: 0x00005D54
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.rowId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.rowId = null;
			}
			physicalColumn = this.createTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.createTime = null;
			}
			physicalColumn = this.propertyBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBlob = null;
			}
			physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.inferenceLogPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.inferenceLogPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400011E RID: 286
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400011F RID: 287
		public const string RowIdName = "RowId";

		// Token: 0x04000120 RID: 288
		public const string CreateTimeName = "CreateTime";

		// Token: 0x04000121 RID: 289
		public const string PropertyBlobName = "PropertyBlob";

		// Token: 0x04000122 RID: 290
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000123 RID: 291
		public const string PhysicalTableName = "InferenceLog";

		// Token: 0x04000124 RID: 292
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000125 RID: 293
		private PhysicalColumn rowId;

		// Token: 0x04000126 RID: 294
		private PhysicalColumn createTime;

		// Token: 0x04000127 RID: 295
		private PhysicalColumn propertyBlob;

		// Token: 0x04000128 RID: 296
		private PhysicalColumn mailboxNumber;

		// Token: 0x04000129 RID: 297
		private Index inferenceLogPK;

		// Token: 0x0400012A RID: 298
		private Table table;
	}
}
