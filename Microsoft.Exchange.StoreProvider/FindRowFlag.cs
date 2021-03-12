using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000049 RID: 73
	[Flags]
	internal enum FindRowFlag
	{
		// Token: 0x04000443 RID: 1091
		None = 0,
		// Token: 0x04000444 RID: 1092
		FindBackward = 1,
		// Token: 0x04000445 RID: 1093
		DeferredErrors = 8,
		// Token: 0x04000446 RID: 1094
		DisableFastFind = 16,
		// Token: 0x04000447 RID: 1095
		DisableSlowFind = 32,
		// Token: 0x04000448 RID: 1096
		DisableQP = 64
	}
}
