using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x0200098F RID: 2447
	[Flags]
	internal enum EnumerateLicenseFlags : uint
	{
		// Token: 0x04002D14 RID: 11540
		Machine = 1U,
		// Token: 0x04002D15 RID: 11541
		GroupIdentity = 2U,
		// Token: 0x04002D16 RID: 11542
		GroupIdentityName = 4U,
		// Token: 0x04002D17 RID: 11543
		GroupIdentityLid = 8U,
		// Token: 0x04002D18 RID: 11544
		SpecifiedGroupIdentity = 16U,
		// Token: 0x04002D19 RID: 11545
		Eul = 32U,
		// Token: 0x04002D1A RID: 11546
		EulLid = 64U,
		// Token: 0x04002D1B RID: 11547
		ClientLicensor = 128U,
		// Token: 0x04002D1C RID: 11548
		ClientLicensorLid = 256U,
		// Token: 0x04002D1D RID: 11549
		SpecifiedClientLicensor = 512U,
		// Token: 0x04002D1E RID: 11550
		RevocationList = 1024U,
		// Token: 0x04002D1F RID: 11551
		RevocationListLid = 2048U,
		// Token: 0x04002D20 RID: 11552
		Expired = 4096U,
		// Token: 0x04002D21 RID: 11553
		IssuanceLicenseTemplate = 16384U,
		// Token: 0x04002D22 RID: 11554
		IssuanceLicenseTemplateLid = 32768U
	}
}
