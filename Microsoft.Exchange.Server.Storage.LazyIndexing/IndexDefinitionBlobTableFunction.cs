using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200000A RID: 10
	public sealed class IndexDefinitionBlobTableFunction
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000354C File Offset: 0x0000174C
		internal IndexDefinitionBlobTableFunction()
		{
			this.columnType = Factory.CreatePhysicalColumn("columnType", "columnType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.maxLength = Factory.CreatePhysicalColumn("maxLength", "maxLength", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.fixedLength = Factory.CreatePhysicalColumn("fixedLength", "fixedLength", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.ascending = Factory.CreatePhysicalColumn("ascending", "ascending", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[4];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.columnType,
				this.maxLength,
				this.fixedLength,
				this.ascending
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("IndexDefinitionBlob", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.columnType,
				this.maxLength,
				this.fixedLength,
				this.ascending
			});
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000036D6 File Offset: 0x000018D6
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000036DE File Offset: 0x000018DE
		public PhysicalColumn ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000036E6 File Offset: 0x000018E6
		public PhysicalColumn MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000036EE File Offset: 0x000018EE
		public PhysicalColumn FixedLength
		{
			get
			{
				return this.fixedLength;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000036F6 File Offset: 0x000018F6
		public PhysicalColumn Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003700 File Offset: 0x00001900
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters[0] == null)
			{
				throw new InvalidSerializedFormatException("Blob must not be null");
			}
			int num;
			int num2;
			short num3;
			return IndexDefinitionBlob.Deserialize(out num, out num2, out num3, (byte[])parameters[0]);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003734 File Offset: 0x00001934
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			IndexDefinitionBlob indexDefinitionBlob = (IndexDefinitionBlob)row;
			if (columnToFetch == this.ColumnType)
			{
				return indexDefinitionBlob.ColumnType;
			}
			if (columnToFetch == this.MaxLength)
			{
				return indexDefinitionBlob.MaxLength;
			}
			if (columnToFetch == this.FixedLength)
			{
				return indexDefinitionBlob.FixedLength;
			}
			if (columnToFetch == this.Ascending)
			{
				return indexDefinitionBlob.Ascending;
			}
			return null;
		}

		// Token: 0x04000030 RID: 48
		public const string columnTypeName = "columnType";

		// Token: 0x04000031 RID: 49
		public const string maxLengthName = "maxLength";

		// Token: 0x04000032 RID: 50
		public const string fixedLengthName = "fixedLength";

		// Token: 0x04000033 RID: 51
		public const string ascendingName = "ascending";

		// Token: 0x04000034 RID: 52
		public const string TableFunctionName = "IndexDefinitionBlob";

		// Token: 0x04000035 RID: 53
		private PhysicalColumn columnType;

		// Token: 0x04000036 RID: 54
		private PhysicalColumn maxLength;

		// Token: 0x04000037 RID: 55
		private PhysicalColumn fixedLength;

		// Token: 0x04000038 RID: 56
		private PhysicalColumn ascending;

		// Token: 0x04000039 RID: 57
		private TableFunction tableFunction;
	}
}
