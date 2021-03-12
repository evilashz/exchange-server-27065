using System;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000021 RID: 33
	[Flags]
	internal enum Pop3State
	{
		// Token: 0x040000A4 RID: 164
		None = 0,
		// Token: 0x040000A5 RID: 165
		Nonauthenticated = 1,
		// Token: 0x040000A6 RID: 166
		User = 2,
		// Token: 0x040000A7 RID: 167
		Pass = 4,
		// Token: 0x040000A8 RID: 168
		Authenticated = 8,
		// Token: 0x040000A9 RID: 169
		Disconnected = 16,
		// Token: 0x040000AA RID: 170
		AuthenticatedButFailed = 32,
		// Token: 0x040000AB RID: 171
		All = 41
	}
}
