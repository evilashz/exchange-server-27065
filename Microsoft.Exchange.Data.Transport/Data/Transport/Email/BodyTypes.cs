using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000D6 RID: 214
	[Flags]
	internal enum BodyTypes
	{
		// Token: 0x04000304 RID: 772
		None = 0,
		// Token: 0x04000305 RID: 773
		Text = 1,
		// Token: 0x04000306 RID: 774
		Enriched = 2,
		// Token: 0x04000307 RID: 775
		Html = 4,
		// Token: 0x04000308 RID: 776
		Calendar = 8
	}
}
