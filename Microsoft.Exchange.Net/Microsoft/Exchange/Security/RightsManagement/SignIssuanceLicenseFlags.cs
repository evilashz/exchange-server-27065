using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000994 RID: 2452
	[Flags]
	internal enum SignIssuanceLicenseFlags : uint
	{
		// Token: 0x04002D3A RID: 11578
		Online = 1U,
		// Token: 0x04002D3B RID: 11579
		Offline = 2U,
		// Token: 0x04002D3C RID: 11580
		Cancel = 4U,
		// Token: 0x04002D3D RID: 11581
		ServerIssuanceLicense = 8U,
		// Token: 0x04002D3E RID: 11582
		AutoGenerateKey = 16U,
		// Token: 0x04002D3F RID: 11583
		OwnerLicenseNoPersist = 32U
	}
}
