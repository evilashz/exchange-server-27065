using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200023C RID: 572
	internal abstract class SearchMailboxAction : ICloneable
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x0004B6FC File Offset: 0x000498FC
		protected static bool PropertyExists(object property)
		{
			return property != null && !(property is PropertyError);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004B70F File Offset: 0x0004990F
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600109A RID: 4250
		public abstract SearchMailboxAction Clone();

		// Token: 0x0600109B RID: 4251
		public abstract void PerformBatchOperation(object[][] batchedItemBuffer, int fetchedItemCount, StoreId currentFolderId, MailboxSession sourceMailbox, MailboxSession targetMailbox, Dictionary<StoreId, FolderNode> folderNodeMap, SearchResultProcessor processor);

		// Token: 0x04000B34 RID: 2868
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x0200023D RID: 573
		protected enum ItemPropertyIndex
		{
			// Token: 0x04000B36 RID: 2870
			Id,
			// Token: 0x04000B37 RID: 2871
			Size,
			// Token: 0x04000B38 RID: 2872
			ParentItemId,
			// Token: 0x04000B39 RID: 2873
			Subject,
			// Token: 0x04000B3A RID: 2874
			IsRead,
			// Token: 0x04000B3B RID: 2875
			SentTime,
			// Token: 0x04000B3C RID: 2876
			ReceivedTime,
			// Token: 0x04000B3D RID: 2877
			Sender,
			// Token: 0x04000B3E RID: 2878
			SenderSmtpAddress
		}
	}
}
