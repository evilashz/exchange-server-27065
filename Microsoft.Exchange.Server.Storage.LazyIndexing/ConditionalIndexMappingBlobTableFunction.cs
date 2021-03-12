using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000007 RID: 7
	public sealed class ConditionalIndexMappingBlobTableFunction
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002B5C File Offset: 0x00000D5C
		internal ConditionalIndexMappingBlobTableFunction()
		{
			this.columnName = Factory.CreatePhysicalColumn("columnName", "columnName", typeof(string), false, false, false, false, false, Visibility.Public, 0, 512, 512);
			this.columnValue = Factory.CreatePhysicalColumn("columnValue", "columnValue", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.columnName,
				this.columnValue
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("ConditionalIndexMappingBlob", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.columnName,
				this.columnValue
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002C7B File Offset: 0x00000E7B
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C83 File Offset: 0x00000E83
		public PhysicalColumn ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002C8B File Offset: 0x00000E8B
		public PhysicalColumn ColumnValue
		{
			get
			{
				return this.columnValue;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C94 File Offset: 0x00000E94
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters[0] == null)
			{
				throw new InvalidSerializedFormatException("Blob must not be null");
			}
			return ConditionalIndexMappingBlob.Deserialize((byte[])parameters[0]);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			ConditionalIndexMappingBlob conditionalIndexMappingBlob = (ConditionalIndexMappingBlob)row;
			if (columnToFetch == this.ColumnName)
			{
				return conditionalIndexMappingBlob.ColumnName;
			}
			if (columnToFetch == this.ColumnValue)
			{
				return conditionalIndexMappingBlob.ColumnValue;
			}
			return null;
		}

		// Token: 0x0400001B RID: 27
		public const string columnNameName = "columnName";

		// Token: 0x0400001C RID: 28
		public const string columnValueName = "columnValue";

		// Token: 0x0400001D RID: 29
		public const string TableFunctionName = "ConditionalIndexMappingBlob";

		// Token: 0x0400001E RID: 30
		private PhysicalColumn columnName;

		// Token: 0x0400001F RID: 31
		private PhysicalColumn columnValue;

		// Token: 0x04000020 RID: 32
		private TableFunction tableFunction;
	}
}
