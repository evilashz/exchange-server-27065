using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007E3 RID: 2019
	internal enum RedirectionOptions
	{
		// Token: 0x040024E8 RID: 9448
		FollowUntilNo302,
		// Token: 0x040024E9 RID: 9449
		StopOnFirstCrossDomainRedirect,
		// Token: 0x040024EA RID: 9450
		FollowUntilNo302OrSpecificRedirection,
		// Token: 0x040024EB RID: 9451
		FollowUntilNo302ExpectCrossDomainOnFirstRedirect
	}
}
