using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000004 RID: 4
	public sealed class ConversationMembersBlobTableFunction
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002538 File Offset: 0x00000738
		internal ConversationMembersBlobTableFunction()
		{
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.messageId = Factory.CreatePhysicalColumn("MessageId", "MessageId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.sortPosition = Factory.CreatePhysicalColumn("SortPosition", "SortPosition", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
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
				this.SortPosition
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("ConversationMembersBlob", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.FolderId,
				this.MessageId,
				this.SortPosition
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002677 File Offset: 0x00000877
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000267F File Offset: 0x0000087F
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002687 File Offset: 0x00000887
		public PhysicalColumn MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000268F File Offset: 0x0000088F
		public PhysicalColumn SortPosition
		{
			get
			{
				return this.sortPosition;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002698 File Offset: 0x00000898
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters[0] == null)
			{
				throw new InvalidSerializedFormatException("Blob must not be null");
			}
			return ConversationMembersBlob.Deserialize((byte[])parameters[0]);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026C4 File Offset: 0x000008C4
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			ConversationMembersBlob conversationMembersBlob = (ConversationMembersBlob)row;
			if (columnToFetch == this.FolderId)
			{
				return conversationMembersBlob.FolderId;
			}
			if (columnToFetch == this.MessageId)
			{
				return conversationMembersBlob.MessageId;
			}
			if (columnToFetch == this.SortPosition)
			{
				return conversationMembersBlob.SortPosition;
			}
			return null;
		}

		// Token: 0x0400000B RID: 11
		public const string FolderIdName = "FolderId";

		// Token: 0x0400000C RID: 12
		public const string MessageIdName = "MessageId";

		// Token: 0x0400000D RID: 13
		public const string SortPositionName = "SortPosition";

		// Token: 0x0400000E RID: 14
		public const string TableFunctionName = "ConversationMembersBlob";

		// Token: 0x0400000F RID: 15
		private PhysicalColumn folderId;

		// Token: 0x04000010 RID: 16
		private PhysicalColumn messageId;

		// Token: 0x04000011 RID: 17
		private PhysicalColumn sortPosition;

		// Token: 0x04000012 RID: 18
		private TableFunction tableFunction;
	}
}
