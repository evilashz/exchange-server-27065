using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000010 RID: 16
	public sealed class PseudoIndexMaintenanceTable
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000048A8 File Offset: 0x00002AA8
		internal PseudoIndexMaintenanceTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.updateRecordNumber = Factory.CreatePhysicalColumn("UpdateRecordNumber", "UpdateRecordNumber", typeof(long), false, true, false, false, false, Visibility.Public, 0, 8, 8);
			this.logicalIndexNumber = Factory.CreatePhysicalColumn("LogicalIndexNumber", "LogicalIndexNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.logicalOperation = Factory.CreatePhysicalColumn("LogicalOperation", "LogicalOperation", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.updatedPropertiesBlob = Factory.CreatePhysicalColumn("UpdatedPropertiesBlob", "UpdatedPropertiesBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "PseudoIndexMaintenancePK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.pseudoIndexMaintenancePK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.UpdateRecordNumber
			});
			Index[] indexes = new Index[]
			{
				this.PseudoIndexMaintenancePK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.UpdateRecordNumber,
				this.LogicalIndexNumber,
				this.LogicalOperation,
				this.UpdatedPropertiesBlob,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("PseudoIndexMaintenance", TableClass.PseudoIndexMaintenance, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Redacted, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004A97 File Offset: 0x00002C97
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004A9F File Offset: 0x00002C9F
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004AA7 File Offset: 0x00002CA7
		public PhysicalColumn UpdateRecordNumber
		{
			get
			{
				return this.updateRecordNumber;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004AAF File Offset: 0x00002CAF
		public PhysicalColumn LogicalIndexNumber
		{
			get
			{
				return this.logicalIndexNumber;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004AB7 File Offset: 0x00002CB7
		public PhysicalColumn LogicalOperation
		{
			get
			{
				return this.logicalOperation;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004ABF File Offset: 0x00002CBF
		public PhysicalColumn UpdatedPropertiesBlob
		{
			get
			{
				return this.updatedPropertiesBlob;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004AC7 File Offset: 0x00002CC7
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004ACF File Offset: 0x00002CCF
		public Index PseudoIndexMaintenancePK
		{
			get
			{
				return this.pseudoIndexMaintenancePK;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004AD8 File Offset: 0x00002CD8
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.updateRecordNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.updateRecordNumber = null;
			}
			physicalColumn = this.logicalIndexNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.logicalIndexNumber = null;
			}
			physicalColumn = this.logicalOperation;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.logicalOperation = null;
			}
			physicalColumn = this.updatedPropertiesBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.updatedPropertiesBlob = null;
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
			Index index = this.pseudoIndexMaintenancePK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.pseudoIndexMaintenancePK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000071 RID: 113
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x04000072 RID: 114
		public const string UpdateRecordNumberName = "UpdateRecordNumber";

		// Token: 0x04000073 RID: 115
		public const string LogicalIndexNumberName = "LogicalIndexNumber";

		// Token: 0x04000074 RID: 116
		public const string LogicalOperationName = "LogicalOperation";

		// Token: 0x04000075 RID: 117
		public const string UpdatedPropertiesBlobName = "UpdatedPropertiesBlob";

		// Token: 0x04000076 RID: 118
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000077 RID: 119
		public const string PhysicalTableName = "PseudoIndexMaintenance";

		// Token: 0x04000078 RID: 120
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000079 RID: 121
		private PhysicalColumn updateRecordNumber;

		// Token: 0x0400007A RID: 122
		private PhysicalColumn logicalIndexNumber;

		// Token: 0x0400007B RID: 123
		private PhysicalColumn logicalOperation;

		// Token: 0x0400007C RID: 124
		private PhysicalColumn updatedPropertiesBlob;

		// Token: 0x0400007D RID: 125
		private PhysicalColumn extensionBlob;

		// Token: 0x0400007E RID: 126
		private Index pseudoIndexMaintenancePK;

		// Token: 0x0400007F RID: 127
		private Table table;
	}
}
