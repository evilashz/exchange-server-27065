using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000019 RID: 25
	[Flags]
	internal enum ExOrgInfoFlags : uint
	{
		// Token: 0x0400007A RID: 122
		None = 0U,
		// Token: 0x0400007B RID: 123
		PublicFoldersEnabled = 1U,
		// Token: 0x0400007C RID: 124
		UseAutoDiscoverForPublicFolderConfiguration = 2U
	}
}
