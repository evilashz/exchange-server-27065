using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F1 RID: 753
	[Flags]
	internal enum LogonFlags : byte
	{
		// Token: 0x04000953 RID: 2387
		None = 0,
		// Token: 0x04000954 RID: 2388
		Private = 1,
		// Token: 0x04000955 RID: 2389
		Undercover = 2,
		// Token: 0x04000956 RID: 2390
		Ghosted = 4,
		// Token: 0x04000957 RID: 2391
		SplProcess = 8,
		// Token: 0x04000958 RID: 2392
		Mapi0 = 16,
		// Token: 0x04000959 RID: 2393
		MbxGuids = 32,
		// Token: 0x0400095A RID: 2394
		Extended = 64,
		// Token: 0x0400095B RID: 2395
		NoRules = 128
	}
}
