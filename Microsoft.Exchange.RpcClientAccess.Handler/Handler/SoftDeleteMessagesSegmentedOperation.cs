using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SoftDeleteMessagesSegmentedOperation : DeleteMessagesSegmentedOperation
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00015553 File Offset: 0x00013753
		internal SoftDeleteMessagesSegmentedOperation(ReferenceCount<CoreFolder> folder, DeleteItemFlags deleteItemFlags, StoreObjectId[] storeObjectIds, int segmentSize, TeamMailboxClientOperations teamMailboxClientOperations) : base(folder, deleteItemFlags, storeObjectIds, segmentSize, teamMailboxClientOperations)
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00015562 File Offset: 0x00013762
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return ((DeleteMessagesResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((DeleteMessagesResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00015595 File Offset: 0x00013795
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulDeleteMessagesResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedDeleteMessagesResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}
	}
}
