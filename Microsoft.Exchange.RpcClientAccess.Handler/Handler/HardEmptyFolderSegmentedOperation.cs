using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class HardEmptyFolderSegmentedOperation : EmptyFolderSegmentedOperation
	{
		// Token: 0x06000226 RID: 550 RVA: 0x000149F7 File Offset: 0x00012BF7
		internal HardEmptyFolderSegmentedOperation(ReferenceCount<CoreFolder> folder, EmptyFolderFlags emptyFolderFlags) : base(folder, emptyFolderFlags)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00014A01 File Offset: 0x00012C01
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode != ErrorCode.None)
			{
				return ((HardEmptyFolderResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((HardEmptyFolderResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00014A34 File Offset: 0x00012C34
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulHardEmptyFolderResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedHardEmptyFolderResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}
	}
}
