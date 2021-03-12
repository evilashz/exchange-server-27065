using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SoftEmptyFolderSegmentedOperation : EmptyFolderSegmentedOperation
	{
		// Token: 0x06000250 RID: 592 RVA: 0x000155C0 File Offset: 0x000137C0
		internal SoftEmptyFolderSegmentedOperation(ReferenceCount<CoreFolder> folder, EmptyFolderFlags emptyFolderFlags) : base(folder, emptyFolderFlags)
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000155CA File Offset: 0x000137CA
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return ((EmptyFolderResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((EmptyFolderResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000155FD File Offset: 0x000137FD
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulEmptyFolderResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedEmptyFolderResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}
	}
}
