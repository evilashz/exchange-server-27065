using System;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000022 RID: 34
	internal enum ExecuteRequestFlag : uint
	{
		// Token: 0x04000093 RID: 147
		NoCompression = 1U,
		// Token: 0x04000094 RID: 148
		NoObfuscation,
		// Token: 0x04000095 RID: 149
		Chain = 4U,
		// Token: 0x04000096 RID: 150
		ExtendedError = 8U
	}
}
