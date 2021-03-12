using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class QuerySegmentEnumerator : SegmentEnumerator
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00014B78 File Offset: 0x00012D78
		public QuerySegmentEnumerator(CoreFolder coreFolder, ItemQueryType itemQueryType, int segmentSize) : base(segmentSize)
		{
			this.queryResult = coreFolder.QueryExecutor.ItemQuery(itemQueryType, null, null, new PropertyDefinition[]
			{
				ItemSchema.Id
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00014BB0 File Offset: 0x00012DB0
		public QuerySegmentEnumerator(CoreFolder coreFolder, FolderQueryFlags folderQueryFlags, int segmentSize) : base(segmentSize)
		{
			this.queryResult = coreFolder.QueryExecutor.FolderQuery(folderQueryFlags, null, null, new PropertyDefinition[]
			{
				FolderSchema.Id
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00014BE8 File Offset: 0x00012DE8
		public override StoreObjectId[] GetNextBatchIds()
		{
			object[][] rows = this.queryResult.GetRows(base.SegmentSize);
			StoreObjectId[] array = new StoreObjectId[rows.Length];
			int num = 0;
			foreach (object[] array3 in rows)
			{
				array[num++] = StoreId.GetStoreObjectId((StoreId)array3[0]);
			}
			return array;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00014C43 File Offset: 0x00012E43
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<QuerySegmentEnumerator>(this);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00014C4B File Offset: 0x00012E4B
		protected override void InternalDispose()
		{
			this.queryResult.Dispose();
			base.InternalDispose();
		}

		// Token: 0x040000CD RID: 205
		private readonly QueryResult queryResult;
	}
}
