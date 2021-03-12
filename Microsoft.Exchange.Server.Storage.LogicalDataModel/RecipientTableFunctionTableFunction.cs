using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000013 RID: 19
	public sealed class RecipientTableFunctionTableFunction
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0002A0A8 File Offset: 0x000282A8
		internal RecipientTableFunctionTableFunction()
		{
			this.recipientBlob = Factory.CreatePhysicalColumn("RecipientBlob", "RecipientBlob", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
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
				this.RecipientBlob
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("RecipientTableFunction", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[0], indexes, new PhysicalColumn[]
			{
				this.RecipientBlob
			});
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0002A16D File Offset: 0x0002836D
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0002A175 File Offset: 0x00028375
		public PhysicalColumn RecipientBlob
		{
			get
			{
				return this.recipientBlob;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0002A17D File Offset: 0x0002837D
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			return new object[0];
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0002A185 File Offset: 0x00028385
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			if (columnToFetch == this.RecipientBlob)
			{
				return null;
			}
			return null;
		}

		// Token: 0x040001C5 RID: 453
		public const string RecipientBlobName = "RecipientBlob";

		// Token: 0x040001C6 RID: 454
		public const string TableFunctionName = "RecipientTableFunction";

		// Token: 0x040001C7 RID: 455
		private PhysicalColumn recipientBlob;

		// Token: 0x040001C8 RID: 456
		private TableFunction tableFunction;
	}
}
