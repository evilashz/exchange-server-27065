using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000015 RID: 21
	public sealed class SearchResultsTableFunction
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0002A7CC File Offset: 0x000289CC
		internal SearchResultsTableFunction()
		{
			this.sortPosition = Factory.CreatePhysicalColumn("SortPosition", "SortPosition", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageDocumentId = Factory.CreatePhysicalColumn("MessageDocumentId", "MessageDocumentId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.conversationDocumentId = Factory.CreatePhysicalColumn("ConversationDocumentId", "ConversationDocumentId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.count = Factory.CreatePhysicalColumn("Count", "Count", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
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
				this.SortPosition,
				this.Count
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("SearchResults", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Redacted, new Type[]
			{
				typeof(IList<SearchFolder.InstantSearchResultsEntry>)
			}, indexes, new PhysicalColumn[]
			{
				this.SortPosition,
				this.MessageDocumentId,
				this.ConversationDocumentId,
				this.Count
			});
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0002A947 File Offset: 0x00028B47
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0002A94F File Offset: 0x00028B4F
		public PhysicalColumn SortPosition
		{
			get
			{
				return this.sortPosition;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0002A957 File Offset: 0x00028B57
		public PhysicalColumn MessageDocumentId
		{
			get
			{
				return this.messageDocumentId;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0002A95F File Offset: 0x00028B5F
		public PhysicalColumn ConversationDocumentId
		{
			get
			{
				return this.conversationDocumentId;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0002A967 File Offset: 0x00028B67
		public PhysicalColumn Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0002A96F File Offset: 0x00028B6F
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			return (IList<SearchFolder.InstantSearchResultsEntry>)parameters[0];
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0002A97C File Offset: 0x00028B7C
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			SearchFolder.InstantSearchResultsEntry instantSearchResultsEntry = (SearchFolder.InstantSearchResultsEntry)row;
			if (columnToFetch == this.SortPosition)
			{
				return instantSearchResultsEntry.SortPosition;
			}
			if (columnToFetch == this.MessageDocumentId)
			{
				return instantSearchResultsEntry.MessageDocumentId;
			}
			if (columnToFetch == this.ConversationDocumentId)
			{
				return instantSearchResultsEntry.ConversationDocumentId;
			}
			if (columnToFetch == this.Count)
			{
				return instantSearchResultsEntry.Count;
			}
			return null;
		}

		// Token: 0x040001DE RID: 478
		public const string SortPositionName = "SortPosition";

		// Token: 0x040001DF RID: 479
		public const string MessageDocumentIdName = "MessageDocumentId";

		// Token: 0x040001E0 RID: 480
		public const string ConversationDocumentIdName = "ConversationDocumentId";

		// Token: 0x040001E1 RID: 481
		public const string CountName = "Count";

		// Token: 0x040001E2 RID: 482
		public const string TableFunctionName = "SearchResults";

		// Token: 0x040001E3 RID: 483
		private PhysicalColumn sortPosition;

		// Token: 0x040001E4 RID: 484
		private PhysicalColumn messageDocumentId;

		// Token: 0x040001E5 RID: 485
		private PhysicalColumn conversationDocumentId;

		// Token: 0x040001E6 RID: 486
		private PhysicalColumn count;

		// Token: 0x040001E7 RID: 487
		private TableFunction tableFunction;
	}
}
