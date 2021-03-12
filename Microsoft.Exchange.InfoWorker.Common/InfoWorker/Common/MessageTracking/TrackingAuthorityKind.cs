using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002B5 RID: 693
	internal enum TrackingAuthorityKind
	{
		// Token: 0x04000CF3 RID: 3315
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = -1)]
		CurrentSite,
		// Token: 0x04000CF4 RID: 3316
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = 500)]
		RemoteSiteInCurrentOrg,
		// Token: 0x04000CF5 RID: 3317
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = 1000)]
		RemoteForest,
		// Token: 0x04000CF6 RID: 3318
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = 2000)]
		RemoteTrustedOrg,
		// Token: 0x04000CF7 RID: 3319
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = -1)]
		RemoteOrg,
		// Token: 0x04000CF8 RID: 3320
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = -1)]
		LegacyExchangeServer,
		// Token: 0x04000CF9 RID: 3321
		[TrackingAuthorityKindInformation(ExpectedConnectionLatencyMSec = -1)]
		Undefined
	}
}
