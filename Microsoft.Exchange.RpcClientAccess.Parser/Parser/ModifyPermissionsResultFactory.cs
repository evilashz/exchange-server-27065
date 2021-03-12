using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000FC RID: 252
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ModifyPermissionsResultFactory : StandardResultFactory
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0000F726 File Offset: 0x0000D926
		internal ModifyPermissionsResultFactory() : base(RopId.ModifyPermissions)
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000F730 File Offset: 0x0000D930
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.ModifyPermissions, ErrorCode.None);
		}
	}
}
