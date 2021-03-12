using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000349 RID: 841
	[Flags]
	internal enum SortOrderFlags : byte
	{
		// Token: 0x04000AB6 RID: 2742
		Ascending = 0,
		// Token: 0x04000AB7 RID: 2743
		Descending = 1,
		// Token: 0x04000AB8 RID: 2744
		Combine = 2,
		// Token: 0x04000AB9 RID: 2745
		CategoryMaximum = 4,
		// Token: 0x04000ABA RID: 2746
		CategoryMinimum = 8
	}
}
