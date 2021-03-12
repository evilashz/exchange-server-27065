using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001AC RID: 428
	[Flags]
	internal enum CanaryStatus
	{
		// Token: 0x04001E0C RID: 7692
		None = 0,
		// Token: 0x04001E0D RID: 7693
		IsCanaryRenewed = 16,
		// Token: 0x04001E0E RID: 7694
		IsCanaryValid = 64,
		// Token: 0x04001E0F RID: 7695
		IsCanaryAboutToExpire = 128,
		// Token: 0x04001E10 RID: 7696
		IsCanaryNeeded = 256
	}
}
