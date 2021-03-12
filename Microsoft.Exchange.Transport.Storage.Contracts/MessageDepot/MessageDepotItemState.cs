using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000006 RID: 6
	internal enum MessageDepotItemState
	{
		// Token: 0x04000009 RID: 9
		Ready,
		// Token: 0x0400000A RID: 10
		Deferred,
		// Token: 0x0400000B RID: 11
		Poisoned,
		// Token: 0x0400000C RID: 12
		Suspended,
		// Token: 0x0400000D RID: 13
		Processing,
		// Token: 0x0400000E RID: 14
		Expiring
	}
}
