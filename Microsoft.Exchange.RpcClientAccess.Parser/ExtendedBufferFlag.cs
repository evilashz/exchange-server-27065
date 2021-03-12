using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004A RID: 74
	[Flags]
	internal enum ExtendedBufferFlag : ushort
	{
		// Token: 0x040000E4 RID: 228
		Compressed = 1,
		// Token: 0x040000E5 RID: 229
		Obfuscated = 2,
		// Token: 0x040000E6 RID: 230
		Last = 4
	}
}
