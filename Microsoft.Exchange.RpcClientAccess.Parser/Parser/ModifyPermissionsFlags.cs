using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000301 RID: 769
	[Flags]
	internal enum ModifyPermissionsFlags : byte
	{
		// Token: 0x040009B7 RID: 2487
		None = 0,
		// Token: 0x040009B8 RID: 2488
		ReplaceRows = 1,
		// Token: 0x040009B9 RID: 2489
		IncludeFreeBusy = 2
	}
}
