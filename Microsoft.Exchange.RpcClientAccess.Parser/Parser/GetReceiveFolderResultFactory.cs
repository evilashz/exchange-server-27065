using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000EB RID: 235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetReceiveFolderResultFactory : StandardResultFactory
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0000F3CA File Offset: 0x0000D5CA
		internal GetReceiveFolderResultFactory() : base(RopId.GetReceiveFolder)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		public RopResult CreateSuccessfulResult(StoreId receiveFolderId, string messageClass)
		{
			return new SuccessfulGetReceiveFolderResult(receiveFolderId, messageClass);
		}
	}
}
