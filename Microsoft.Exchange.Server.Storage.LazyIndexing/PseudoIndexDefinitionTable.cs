using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200000F RID: 15
	public sealed class PseudoIndexDefinitionTable
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000045C4 File Offset: 0x000027C4
		internal PseudoIndexDefinitionTable()
		{
			this.physicalIndexNumber = Factory.CreatePhysicalColumn("PhysicalIndexNumber", "PhysicalIndexNumber", typeof(int), false, true, false, false, false, Visibility.Public, 0, 4, 4);
			this.columnBlob = Factory.CreatePhysicalColumn("ColumnBlob", "ColumnBlob", typeof(byte[]), false, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "PseudoIndexDefinitionPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.pseudoIndexDefinitionPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.PhysicalIndexNumber
			});
			Index[] indexes = new Index[]
			{
				this.PseudoIndexDefinitionPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.PhysicalIndexNumber,
				this.ColumnBlob,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("PseudoIndexDefinition", TableClass.PseudoIndexDefinition, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000470F File Offset: 0x0000290F
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004717 File Offset: 0x00002917
		public PhysicalColumn PhysicalIndexNumber
		{
			get
			{
				return this.physicalIndexNumber;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000471F File Offset: 0x0000291F
		public PhysicalColumn ColumnBlob
		{
			get
			{
				return this.columnBlob;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004727 File Offset: 0x00002927
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000472F File Offset: 0x0000292F
		public Index PseudoIndexDefinitionPK
		{
			get
			{
				return this.pseudoIndexDefinitionPK;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004738 File Offset: 0x00002938
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.physicalIndexNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.physicalIndexNumber = null;
			}
			physicalColumn = this.columnBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.columnBlob = null;
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
			Index index = this.pseudoIndexDefinitionPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.pseudoIndexDefinitionPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000068 RID: 104
		public const string PhysicalIndexNumberName = "PhysicalIndexNumber";

		// Token: 0x04000069 RID: 105
		public const string ColumnBlobName = "ColumnBlob";

		// Token: 0x0400006A RID: 106
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x0400006B RID: 107
		public const string PhysicalTableName = "PseudoIndexDefinition";

		// Token: 0x0400006C RID: 108
		private PhysicalColumn physicalIndexNumber;

		// Token: 0x0400006D RID: 109
		private PhysicalColumn columnBlob;

		// Token: 0x0400006E RID: 110
		private PhysicalColumn extensionBlob;

		// Token: 0x0400006F RID: 111
		private Index pseudoIndexDefinitionPK;

		// Token: 0x04000070 RID: 112
		private Table table;
	}
}
