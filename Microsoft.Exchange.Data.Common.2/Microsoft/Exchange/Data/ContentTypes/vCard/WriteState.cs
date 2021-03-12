using System;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000C3 RID: 195
	[Flags]
	internal enum WriteState
	{
		// Token: 0x04000674 RID: 1652
		Start = 1,
		// Token: 0x04000675 RID: 1653
		Component = 2,
		// Token: 0x04000676 RID: 1654
		Property = 4,
		// Token: 0x04000677 RID: 1655
		Parameter = 8,
		// Token: 0x04000678 RID: 1656
		Closed = 16
	}
}
