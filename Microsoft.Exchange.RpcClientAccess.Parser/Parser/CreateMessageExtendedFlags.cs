using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000049 RID: 73
	[Flags]
	internal enum CreateMessageExtendedFlags : uint
	{
		// Token: 0x040000E0 RID: 224
		None = 0U,
		// Token: 0x040000E1 RID: 225
		ContentAggregation = 1U,
		// Token: 0x040000E2 RID: 226
		ClientAssociated = 64U
	}
}
