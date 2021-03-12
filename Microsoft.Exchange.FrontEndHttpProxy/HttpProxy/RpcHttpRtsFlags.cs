using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000DB RID: 219
	[Flags]
	internal enum RpcHttpRtsFlags
	{
		// Token: 0x040004E2 RID: 1250
		None = 0,
		// Token: 0x040004E3 RID: 1251
		Ping = 1,
		// Token: 0x040004E4 RID: 1252
		OtherCommand = 2,
		// Token: 0x040004E5 RID: 1253
		RecycleChannel = 4,
		// Token: 0x040004E6 RID: 1254
		InChannel = 8,
		// Token: 0x040004E7 RID: 1255
		OutChannel = 16,
		// Token: 0x040004E8 RID: 1256
		EndOfFile = 32,
		// Token: 0x040004E9 RID: 1257
		Echo = 64
	}
}
