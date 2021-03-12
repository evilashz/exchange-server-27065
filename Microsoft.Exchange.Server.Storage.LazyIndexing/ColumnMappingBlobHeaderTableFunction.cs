using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000004 RID: 4
	public sealed class ColumnMappingBlobHeaderTableFunction
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002780 File Offset: 0x00000980
		internal ColumnMappingBlobHeaderTableFunction()
		{
			this.keyColumnCount = Factory.CreatePhysicalColumn("keyColumnCount", "keyColumnCount", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.keyColumnCount
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("ColumnMappingBlobHeader", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.keyColumnCount
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002857 File Offset: 0x00000A57
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000285F File Offset: 0x00000A5F
		public PhysicalColumn KeyColumnCount
		{
			get
			{
				return this.keyColumnCount;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002868 File Offset: 0x00000A68
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters[0] == null)
			{
				throw new InvalidSerializedFormatException("Blob must not be null");
			}
			int num;
			ColumnMappingBlob.Deserialize(out num, (byte[])parameters[0]);
			return new ColumnMappingBlobHeaderTableFunction.ColumnMappingBlobHeader[]
			{
				new ColumnMappingBlobHeaderTableFunction.ColumnMappingBlobHeader(num)
			};
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028A8 File Offset: 0x00000AA8
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			ColumnMappingBlobHeaderTableFunction.ColumnMappingBlobHeader columnMappingBlobHeader = (ColumnMappingBlobHeaderTableFunction.ColumnMappingBlobHeader)row;
			if (columnToFetch == this.KeyColumnCount)
			{
				return columnMappingBlobHeader.KeyColumnCount;
			}
			return null;
		}

		// Token: 0x04000013 RID: 19
		public const string keyColumnCountName = "keyColumnCount";

		// Token: 0x04000014 RID: 20
		public const string TableFunctionName = "ColumnMappingBlobHeader";

		// Token: 0x04000015 RID: 21
		private PhysicalColumn keyColumnCount;

		// Token: 0x04000016 RID: 22
		private TableFunction tableFunction;

		// Token: 0x02000005 RID: 5
		private class ColumnMappingBlobHeader
		{
			// Token: 0x06000019 RID: 25 RVA: 0x000028D7 File Offset: 0x00000AD7
			public ColumnMappingBlobHeader(int keyColumnCount)
			{
				this.keyColumnCount = keyColumnCount;
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600001A RID: 26 RVA: 0x000028E6 File Offset: 0x00000AE6
			public int KeyColumnCount
			{
				get
				{
					return this.keyColumnCount;
				}
			}

			// Token: 0x04000017 RID: 23
			private readonly int keyColumnCount;
		}
	}
}
