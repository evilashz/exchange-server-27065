using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000048 RID: 72
	[Flags]
	internal enum CopyPropertiesFlags : byte
	{
		// Token: 0x040000DC RID: 220
		None = 0,
		// Token: 0x040000DD RID: 221
		Move = 1,
		// Token: 0x040000DE RID: 222
		NoReplace = 2
	}
}
