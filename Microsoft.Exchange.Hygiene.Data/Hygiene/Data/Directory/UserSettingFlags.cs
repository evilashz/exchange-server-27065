using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000117 RID: 279
	[Flags]
	internal enum UserSettingFlags
	{
		// Token: 0x0400057F RID: 1407
		None = 0,
		// Token: 0x04000580 RID: 1408
		IsOutbound = 1,
		// Token: 0x04000581 RID: 1409
		SpamEnabled = 2,
		// Token: 0x04000582 RID: 1410
		PolicyEnabled = 4,
		// Token: 0x04000583 RID: 1411
		VirusEnabled = 8,
		// Token: 0x04000584 RID: 1412
		Archive = 16
	}
}
