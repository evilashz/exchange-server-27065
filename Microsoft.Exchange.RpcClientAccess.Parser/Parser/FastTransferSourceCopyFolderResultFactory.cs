using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D0 RID: 208
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSourceCopyFolderResultFactory : StandardResultFactory
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x0000EF44 File Offset: 0x0000D144
		internal FastTransferSourceCopyFolderResultFactory() : base(RopId.FastTransferSourceCopyFolder)
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000EF4E File Offset: 0x0000D14E
		public SuccessfulFastTransferSourceCopyFolderResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulFastTransferSourceCopyFolderResult(serverObject);
		}
	}
}
