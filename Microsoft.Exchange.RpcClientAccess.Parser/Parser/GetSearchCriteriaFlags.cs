using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D9 RID: 729
	[Flags]
	internal enum GetSearchCriteriaFlags : byte
	{
		// Token: 0x0400084A RID: 2122
		None = 0,
		// Token: 0x0400084B RID: 2123
		Unicode = 1,
		// Token: 0x0400084C RID: 2124
		Restriction = 2,
		// Token: 0x0400084D RID: 2125
		FolderIds = 4
	}
}
