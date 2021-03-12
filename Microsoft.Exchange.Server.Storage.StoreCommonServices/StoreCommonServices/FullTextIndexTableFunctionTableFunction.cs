using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000004 RID: 4
	public sealed class FullTextIndexTableFunctionTableFunction
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002A7C File Offset: 0x00000C7C
		internal FullTextIndexTableFunctionTableFunction()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageDocumentId = Factory.CreatePhysicalColumn("MessageDocumentId", "MessageDocumentId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
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
				this.MailboxPartitionNumber,
				this.MessageDocumentId
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("FullTextIndexTableFunction", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Redacted, new Type[]
			{
				typeof(StoreFullTextIndexQuery)
			}, indexes, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.MessageDocumentId
			});
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002B93 File Offset: 0x00000D93
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002B9B File Offset: 0x00000D9B
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public PhysicalColumn MessageDocumentId
		{
			get
			{
				return this.messageDocumentId;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002BAC File Offset: 0x00000DAC
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			return (StoreFullTextIndexQuery)parameters[0];
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			FullTextIndexRow fullTextIndexRow = (FullTextIndexRow)row;
			if (columnToFetch == this.MailboxPartitionNumber)
			{
				return fullTextIndexRow.MailboxNumber;
			}
			if (columnToFetch == this.MessageDocumentId)
			{
				return fullTextIndexRow.DocumentId;
			}
			return null;
		}

		// Token: 0x04000019 RID: 25
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400001A RID: 26
		public const string MessageDocumentIdName = "MessageDocumentId";

		// Token: 0x0400001B RID: 27
		public const string TableFunctionName = "FullTextIndexTableFunction";

		// Token: 0x0400001C RID: 28
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x0400001D RID: 29
		private PhysicalColumn messageDocumentId;

		// Token: 0x0400001E RID: 30
		private TableFunction tableFunction;
	}
}
