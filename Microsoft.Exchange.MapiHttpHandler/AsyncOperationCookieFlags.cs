using System;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000037 RID: 55
	[Flags]
	internal enum AsyncOperationCookieFlags
	{
		// Token: 0x040000D8 RID: 216
		None = 0,
		// Token: 0x040000D9 RID: 217
		RequireSession = 1,
		// Token: 0x040000DA RID: 218
		AllowSession = 2,
		// Token: 0x040000DB RID: 219
		CreateSession = 4,
		// Token: 0x040000DC RID: 220
		RequireSequence = 8,
		// Token: 0x040000DD RID: 221
		AllowInvalidSession = 16,
		// Token: 0x040000DE RID: 222
		DestroySession = 32
	}
}
