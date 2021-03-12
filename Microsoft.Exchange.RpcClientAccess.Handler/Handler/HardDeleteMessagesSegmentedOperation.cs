using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class HardDeleteMessagesSegmentedOperation : DeleteMessagesSegmentedOperation
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0001498A File Offset: 0x00012B8A
		internal HardDeleteMessagesSegmentedOperation(ReferenceCount<CoreFolder> folder, DeleteItemFlags deleteItemFlags, StoreObjectId[] storeObjectIds, int segmentSize, TeamMailboxClientOperations teamMailboxClientOperations) : base(folder, deleteItemFlags, storeObjectIds, segmentSize, teamMailboxClientOperations)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00014999 File Offset: 0x00012B99
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode != ErrorCode.None)
			{
				return ((HardDeleteMessagesResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((HardDeleteMessagesResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000149CC File Offset: 0x00012BCC
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulHardDeleteMessagesResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedHardDeleteMessagesResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}
	}
}
