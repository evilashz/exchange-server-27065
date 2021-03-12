using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200000B RID: 11
	public sealed class IndexDefinitionBlobHeaderTableFunction
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000037BC File Offset: 0x000019BC
		internal IndexDefinitionBlobHeaderTableFunction()
		{
			this.keyColumnCount = Factory.CreatePhysicalColumn("keyColumnCount", "keyColumnCount", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.lcid = Factory.CreatePhysicalColumn("lcid", "lcid", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.identityColumnIndex = Factory.CreatePhysicalColumn("identityColumnIndex", "identityColumnIndex", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[3];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.keyColumnCount,
				this.lcid,
				this.identityColumnIndex
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("IndexDefinitionBlobHeader", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.keyColumnCount,
				this.lcid,
				this.identityColumnIndex
			});
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000390B File Offset: 0x00001B0B
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003913 File Offset: 0x00001B13
		public PhysicalColumn KeyColumnCount
		{
			get
			{
				return this.keyColumnCount;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000391B File Offset: 0x00001B1B
		public PhysicalColumn Lcid
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003923 File Offset: 0x00001B23
		public PhysicalColumn IdentityColumnIndex
		{
			get
			{
				return this.identityColumnIndex;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000392C File Offset: 0x00001B2C
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters[0] == null)
			{
				throw new InvalidSerializedFormatException("Blob must not be null");
			}
			int num;
			int num2;
			short num3;
			IndexDefinitionBlob.Deserialize(out num, out num2, out num3, (byte[])parameters[0]);
			return new IndexDefinitionBlobHeaderTableFunction.IndexDefinitionBlobHeader[]
			{
				new IndexDefinitionBlobHeaderTableFunction.IndexDefinitionBlobHeader(num, num2, num3)
			};
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003970 File Offset: 0x00001B70
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			IndexDefinitionBlobHeaderTableFunction.IndexDefinitionBlobHeader indexDefinitionBlobHeader = (IndexDefinitionBlobHeaderTableFunction.IndexDefinitionBlobHeader)row;
			if (columnToFetch == this.KeyColumnCount)
			{
				return indexDefinitionBlobHeader.KeyColumnCount;
			}
			if (columnToFetch == this.Lcid)
			{
				return indexDefinitionBlobHeader.Lcid;
			}
			if (columnToFetch == this.IdentityColumnIndex)
			{
				return indexDefinitionBlobHeader.IdentityColumnIndex;
			}
			return null;
		}

		// Token: 0x0400003A RID: 58
		public const string keyColumnCountName = "keyColumnCount";

		// Token: 0x0400003B RID: 59
		public const string lcidName = "lcid";

		// Token: 0x0400003C RID: 60
		public const string identityColumnIndexName = "identityColumnIndex";

		// Token: 0x0400003D RID: 61
		public const string TableFunctionName = "IndexDefinitionBlobHeader";

		// Token: 0x0400003E RID: 62
		private PhysicalColumn keyColumnCount;

		// Token: 0x0400003F RID: 63
		private PhysicalColumn lcid;

		// Token: 0x04000040 RID: 64
		private PhysicalColumn identityColumnIndex;

		// Token: 0x04000041 RID: 65
		private TableFunction tableFunction;

		// Token: 0x0200000C RID: 12
		private class IndexDefinitionBlobHeader
		{
			// Token: 0x0600004E RID: 78 RVA: 0x000039D3 File Offset: 0x00001BD3
			public IndexDefinitionBlobHeader(int keyColumnCount, int lcid, short identityColumnIndex)
			{
				this.keyColumnCount = keyColumnCount;
				this.lcid = lcid;
				this.identityColumnIndex = identityColumnIndex;
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x0600004F RID: 79 RVA: 0x000039F0 File Offset: 0x00001BF0
			public int KeyColumnCount
			{
				get
				{
					return this.keyColumnCount;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000050 RID: 80 RVA: 0x000039F8 File Offset: 0x00001BF8
			public int Lcid
			{
				get
				{
					return this.lcid;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000051 RID: 81 RVA: 0x00003A00 File Offset: 0x00001C00
			public short IdentityColumnIndex
			{
				get
				{
					return this.identityColumnIndex;
				}
			}

			// Token: 0x04000042 RID: 66
			private readonly int keyColumnCount;

			// Token: 0x04000043 RID: 67
			private readonly int lcid;

			// Token: 0x04000044 RID: 68
			private readonly short identityColumnIndex;
		}
	}
}
