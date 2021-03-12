using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001EE RID: 494
	internal enum PhishingLevel
	{
		// Token: 0x04000DCA RID: 3530
		Pass = 1,
		// Token: 0x04000DCB RID: 3531
		Neutral,
		// Token: 0x04000DCC RID: 3532
		SoftFail,
		// Token: 0x04000DCD RID: 3533
		Suspicious1,
		// Token: 0x04000DCE RID: 3534
		Suspicious2,
		// Token: 0x04000DCF RID: 3535
		Suspicious3,
		// Token: 0x04000DD0 RID: 3536
		Suspicious4
	}
}
