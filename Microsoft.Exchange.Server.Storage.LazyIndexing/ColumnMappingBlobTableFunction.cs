using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000003 RID: 3
	public sealed class ColumnMappingBlobTableFunction
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000024B8 File Offset: 0x000006B8
		internal ColumnMappingBlobTableFunction()
		{
			this.columnType = Factory.CreatePhysicalColumn("columnType", "columnType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.fixedLength = Factory.CreatePhysicalColumn("fixedLength", "fixedLength", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.columnLength = Factory.CreatePhysicalColumn("columnLength", "columnLength", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.propName = Factory.CreatePhysicalColumn("propName", "propName", typeof(string), false, false, false, false, false, Visibility.Public, 0, 512, 512);
			this.propId = Factory.CreatePhysicalColumn("propId", "propId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[5];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.columnType,
				this.fixedLength,
				this.columnLength,
				this.propName,
				this.propId
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("ColumnMappingBlob", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.columnType,
				this.fixedLength,
				this.columnLength,
				this.propName,
				this.propId
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002685 File Offset: 0x00000885
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000268D File Offset: 0x0000088D
		public PhysicalColumn ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002695 File Offset: 0x00000895
		public PhysicalColumn FixedLength
		{
			get
			{
				return this.fixedLength;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000269D File Offset: 0x0000089D
		public PhysicalColumn ColumnLength
		{
			get
			{
				return this.columnLength;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000026A5 File Offset: 0x000008A5
		public PhysicalColumn PropName
		{
			get
			{
				return this.propName;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000026AD File Offset: 0x000008AD
		public PhysicalColumn PropId
		{
			get
			{
				return this.propId;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026B8 File Offset: 0x000008B8
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters[0] == null)
			{
				throw new InvalidSerializedFormatException("Blob must not be null");
			}
			int num;
			return ColumnMappingBlob.Deserialize(out num, (byte[])parameters[0]);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026E8 File Offset: 0x000008E8
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			ColumnMappingBlob columnMappingBlob = (ColumnMappingBlob)row;
			if (columnToFetch == this.ColumnType)
			{
				return columnMappingBlob.ColumnType;
			}
			if (columnToFetch == this.FixedLength)
			{
				return columnMappingBlob.FixedLength;
			}
			if (columnToFetch == this.ColumnLength)
			{
				return columnMappingBlob.ColumnLength;
			}
			if (columnToFetch == this.PropName)
			{
				return columnMappingBlob.PropName;
			}
			if (columnToFetch == this.PropId)
			{
				return columnMappingBlob.PropId;
			}
			return null;
		}

		// Token: 0x04000007 RID: 7
		public const string columnTypeName = "columnType";

		// Token: 0x04000008 RID: 8
		public const string fixedLengthName = "fixedLength";

		// Token: 0x04000009 RID: 9
		public const string columnLengthName = "columnLength";

		// Token: 0x0400000A RID: 10
		public const string propNameName = "propName";

		// Token: 0x0400000B RID: 11
		public const string propIdName = "propId";

		// Token: 0x0400000C RID: 12
		public const string TableFunctionName = "ColumnMappingBlob";

		// Token: 0x0400000D RID: 13
		private PhysicalColumn columnType;

		// Token: 0x0400000E RID: 14
		private PhysicalColumn fixedLength;

		// Token: 0x0400000F RID: 15
		private PhysicalColumn columnLength;

		// Token: 0x04000010 RID: 16
		private PhysicalColumn propName;

		// Token: 0x04000011 RID: 17
		private PhysicalColumn propId;

		// Token: 0x04000012 RID: 18
		private TableFunction tableFunction;
	}
}
