using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000297 RID: 663
	[Flags]
	internal enum DeleteFolderFlags : byte
	{
		// Token: 0x0400077D RID: 1917
		DeleteMessages = 1,
		// Token: 0x0400077E RID: 1918
		DeleteFolders = 4,
		// Token: 0x0400077F RID: 1919
		DeleteAssociated = 8,
		// Token: 0x04000780 RID: 1920
		HardDelete = 16,
		// Token: 0x04000781 RID: 1921
		Force = 32
	}
}
