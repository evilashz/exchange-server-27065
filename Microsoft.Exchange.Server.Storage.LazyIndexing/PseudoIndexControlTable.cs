using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200000E RID: 14
	public sealed class PseudoIndexControlTable
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003CBC File Offset: 0x00001EBC
		internal PseudoIndexControlTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.logicalIndexNumber = Factory.CreatePhysicalColumn("LogicalIndexNumber", "LogicalIndexNumber", typeof(int), false, true, false, false, false, Visibility.Public, 0, 4, 4);
			this.indexType = Factory.CreatePhysicalColumn("IndexType", "IndexType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.physicalIndexNumber = Factory.CreatePhysicalColumn("PhysicalIndexNumber", "PhysicalIndexNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.firstUpdateRecord = Factory.CreatePhysicalColumn("FirstUpdateRecord", "FirstUpdateRecord", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastReferenceDate = Factory.CreatePhysicalColumn("LastReferenceDate", "LastReferenceDate", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.columnMappings = Factory.CreatePhysicalColumn("ColumnMappings", "ColumnMappings", typeof(byte[]), false, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.conditionalIndex = Factory.CreatePhysicalColumn("ConditionalIndex", "ConditionalIndex", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.tableName = Factory.CreatePhysicalColumn("TableName", "TableName", typeof(string), false, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.categorizationInfo = Factory.CreatePhysicalColumn("CategorizationInfo", "CategorizationInfo", typeof(byte[]), true, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.logicalIndexVersion = Factory.CreatePhysicalColumn("LogicalIndexVersion", "LogicalIndexVersion", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.indexSignature = Factory.CreatePhysicalColumn("IndexSignature", "IndexSignature", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			string name = "PseudoIndexControlPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[3];
			this.pseudoIndexControlPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId,
				this.LogicalIndexNumber
			});
			Index[] indexes = new Index[]
			{
				this.PseudoIndexControlPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId,
				this.LogicalIndexNumber,
				this.IndexType,
				this.PhysicalIndexNumber,
				this.FirstUpdateRecord,
				this.LastReferenceDate,
				this.ColumnMappings,
				this.ConditionalIndex,
				this.TableName,
				this.CategorizationInfo,
				this.LogicalIndexVersion,
				this.ExtensionBlob,
				this.IndexSignature
			};
			this.table = Factory.CreateTable("PseudoIndexControl", TableClass.PseudoIndexControl, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004063 File Offset: 0x00002263
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000406B File Offset: 0x0000226B
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004073 File Offset: 0x00002273
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000407B File Offset: 0x0000227B
		public PhysicalColumn LogicalIndexNumber
		{
			get
			{
				return this.logicalIndexNumber;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004083 File Offset: 0x00002283
		public PhysicalColumn IndexType
		{
			get
			{
				return this.indexType;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000408B File Offset: 0x0000228B
		public PhysicalColumn PhysicalIndexNumber
		{
			get
			{
				return this.physicalIndexNumber;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004093 File Offset: 0x00002293
		public PhysicalColumn FirstUpdateRecord
		{
			get
			{
				return this.firstUpdateRecord;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000409B File Offset: 0x0000229B
		public PhysicalColumn LastReferenceDate
		{
			get
			{
				return this.lastReferenceDate;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000040A3 File Offset: 0x000022A3
		public PhysicalColumn ColumnMappings
		{
			get
			{
				return this.columnMappings;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000040AB File Offset: 0x000022AB
		public PhysicalColumn ConditionalIndex
		{
			get
			{
				return this.conditionalIndex;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000040B3 File Offset: 0x000022B3
		public PhysicalColumn TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000040BB File Offset: 0x000022BB
		public PhysicalColumn CategorizationInfo
		{
			get
			{
				return this.categorizationInfo;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000040C3 File Offset: 0x000022C3
		public PhysicalColumn LogicalIndexVersion
		{
			get
			{
				return this.logicalIndexVersion;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000040CB File Offset: 0x000022CB
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000040D3 File Offset: 0x000022D3
		public PhysicalColumn IndexSignature
		{
			get
			{
				return this.indexSignature;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000040DB File Offset: 0x000022DB
		public Index PseudoIndexControlPK
		{
			get
			{
				return this.pseudoIndexControlPK;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000040E4 File Offset: 0x000022E4
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.folderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.folderId = null;
			}
			physicalColumn = this.logicalIndexNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.logicalIndexNumber = null;
			}
			physicalColumn = this.indexType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.indexType = null;
			}
			physicalColumn = this.physicalIndexNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.physicalIndexNumber = null;
			}
			physicalColumn = this.firstUpdateRecord;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.firstUpdateRecord = null;
			}
			physicalColumn = this.lastReferenceDate;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastReferenceDate = null;
			}
			physicalColumn = this.columnMappings;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.columnMappings = null;
			}
			physicalColumn = this.conditionalIndex;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conditionalIndex = null;
			}
			physicalColumn = this.tableName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.tableName = null;
			}
			physicalColumn = this.categorizationInfo;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.categorizationInfo = null;
			}
			physicalColumn = this.logicalIndexVersion;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.logicalIndexVersion = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.indexSignature;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.indexSignature = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.pseudoIndexControlPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.pseudoIndexControlPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000049 RID: 73
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400004A RID: 74
		public const string FolderIdName = "FolderId";

		// Token: 0x0400004B RID: 75
		public const string LogicalIndexNumberName = "LogicalIndexNumber";

		// Token: 0x0400004C RID: 76
		public const string IndexTypeName = "IndexType";

		// Token: 0x0400004D RID: 77
		public const string PhysicalIndexNumberName = "PhysicalIndexNumber";

		// Token: 0x0400004E RID: 78
		public const string FirstUpdateRecordName = "FirstUpdateRecord";

		// Token: 0x0400004F RID: 79
		public const string LastReferenceDateName = "LastReferenceDate";

		// Token: 0x04000050 RID: 80
		public const string ColumnMappingsName = "ColumnMappings";

		// Token: 0x04000051 RID: 81
		public const string ConditionalIndexName = "ConditionalIndex";

		// Token: 0x04000052 RID: 82
		public const string TableNameName = "TableName";

		// Token: 0x04000053 RID: 83
		public const string CategorizationInfoName = "CategorizationInfo";

		// Token: 0x04000054 RID: 84
		public const string LogicalIndexVersionName = "LogicalIndexVersion";

		// Token: 0x04000055 RID: 85
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000056 RID: 86
		public const string IndexSignatureName = "IndexSignature";

		// Token: 0x04000057 RID: 87
		public const string PhysicalTableName = "PseudoIndexControl";

		// Token: 0x04000058 RID: 88
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000059 RID: 89
		private PhysicalColumn folderId;

		// Token: 0x0400005A RID: 90
		private PhysicalColumn logicalIndexNumber;

		// Token: 0x0400005B RID: 91
		private PhysicalColumn indexType;

		// Token: 0x0400005C RID: 92
		private PhysicalColumn physicalIndexNumber;

		// Token: 0x0400005D RID: 93
		private PhysicalColumn firstUpdateRecord;

		// Token: 0x0400005E RID: 94
		private PhysicalColumn lastReferenceDate;

		// Token: 0x0400005F RID: 95
		private PhysicalColumn columnMappings;

		// Token: 0x04000060 RID: 96
		private PhysicalColumn conditionalIndex;

		// Token: 0x04000061 RID: 97
		private PhysicalColumn tableName;

		// Token: 0x04000062 RID: 98
		private PhysicalColumn categorizationInfo;

		// Token: 0x04000063 RID: 99
		private PhysicalColumn logicalIndexVersion;

		// Token: 0x04000064 RID: 100
		private PhysicalColumn extensionBlob;

		// Token: 0x04000065 RID: 101
		private PhysicalColumn indexSignature;

		// Token: 0x04000066 RID: 102
		private Index pseudoIndexControlPK;

		// Token: 0x04000067 RID: 103
		private Table table;
	}
}
