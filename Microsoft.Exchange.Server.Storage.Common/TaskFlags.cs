using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200008B RID: 139
	[Flags]
	public enum TaskFlags : byte
	{
		// Token: 0x0400068A RID: 1674
		None = 0,
		// Token: 0x0400068B RID: 1675
		AutoStart = 1,
		// Token: 0x0400068C RID: 1676
		UseThreadPoolThread = 2,
		// Token: 0x0400068D RID: 1677
		RunOnceOnly = 4
	}
}
