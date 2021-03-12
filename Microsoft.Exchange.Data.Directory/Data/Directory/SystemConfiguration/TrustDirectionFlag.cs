using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000332 RID: 818
	[Flags]
	internal enum TrustDirectionFlag
	{
		// Token: 0x0400171C RID: 5916
		None = 0,
		// Token: 0x0400171D RID: 5917
		Inbound = 1,
		// Token: 0x0400171E RID: 5918
		Outbound = 2,
		// Token: 0x0400171F RID: 5919
		Bidirectional = 3
	}
}
