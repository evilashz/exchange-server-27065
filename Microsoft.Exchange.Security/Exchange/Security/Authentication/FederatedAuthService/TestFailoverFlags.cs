using System;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200005B RID: 91
	[Flags]
	internal enum TestFailoverFlags
	{
		// Token: 0x04000290 RID: 656
		None = 0,
		// Token: 0x04000291 RID: 657
		HRDRequest = 1,
		// Token: 0x04000292 RID: 658
		HRDResponse = 2,
		// Token: 0x04000293 RID: 659
		LiveIdRequest = 4,
		// Token: 0x04000294 RID: 660
		LiveIdResponse = 8,
		// Token: 0x04000295 RID: 661
		OrgIdRequest = 16,
		// Token: 0x04000296 RID: 662
		OrgIdResponse = 32,
		// Token: 0x04000297 RID: 663
		OfflineHRD = 64,
		// Token: 0x04000298 RID: 664
		OfflineAuthentication = 128,
		// Token: 0x04000299 RID: 665
		HRDRequestTimeout = 256,
		// Token: 0x0400029A RID: 666
		LiveIdRequestTimeout = 512,
		// Token: 0x0400029B RID: 667
		LowPasswordConfidence = 1024,
		// Token: 0x0400029C RID: 668
		OrgIdRequestTimeout = 2048,
		// Token: 0x0400029D RID: 669
		FederatedRequestTimeout = 4096,
		// Token: 0x0400029E RID: 670
		FederatedRequest = 8192
	}
}
