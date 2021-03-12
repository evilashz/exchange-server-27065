using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000D7 RID: 215
	[Flags]
	internal enum MessageFlags
	{
		// Token: 0x0400030A RID: 778
		None = 0,
		// Token: 0x0400030B RID: 779
		Normal = 1,
		// Token: 0x0400030C RID: 780
		System = 2,
		// Token: 0x0400030D RID: 781
		KnownApplication = 4,
		// Token: 0x0400030E RID: 782
		Tnef = 8
	}
}
