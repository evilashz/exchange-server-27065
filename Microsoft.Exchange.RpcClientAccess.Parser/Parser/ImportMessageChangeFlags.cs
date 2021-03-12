using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200005B RID: 91
	[Flags]
	internal enum ImportMessageChangeFlags : byte
	{
		// Token: 0x04000120 RID: 288
		None = 0,
		// Token: 0x04000121 RID: 289
		Associated = 16,
		// Token: 0x04000122 RID: 290
		FailOnConflict = 64
	}
}
