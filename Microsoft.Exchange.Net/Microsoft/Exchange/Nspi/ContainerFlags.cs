using System;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x0200091A RID: 2330
	[Flags]
	internal enum ContainerFlags
	{
		// Token: 0x04002B66 RID: 11110
		None = 0,
		// Token: 0x04002B67 RID: 11111
		Recipients = 1,
		// Token: 0x04002B68 RID: 11112
		Subcontainers = 2,
		// Token: 0x04002B69 RID: 11113
		Unmodifiable = 8,
		// Token: 0x04002B6A RID: 11114
		ConfRooms = 512
	}
}
