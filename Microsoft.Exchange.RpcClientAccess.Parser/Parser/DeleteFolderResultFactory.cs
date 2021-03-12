using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeleteFolderResultFactory : ResultFactory
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		internal DeleteFolderResultFactory()
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000EC32 File Offset: 0x0000CE32
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000EC3B File Offset: 0x0000CE3B
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new DeleteFolderResult(errorCode, false);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000EC44 File Offset: 0x0000CE44
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new DeleteFolderResult(ErrorCode.None, isPartiallyCompleted);
		}
	}
}
