using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000310 RID: 784
	internal enum AmDbActionStatus
	{
		// Token: 0x040014AB RID: 5291
		None = 1,
		// Token: 0x040014AC RID: 5292
		Started,
		// Token: 0x040014AD RID: 5293
		Completed,
		// Token: 0x040014AE RID: 5294
		Cancelled,
		// Token: 0x040014AF RID: 5295
		Failed,
		// Token: 0x040014B0 RID: 5296
		AcllInitiated,
		// Token: 0x040014B1 RID: 5297
		AcllSuccessful,
		// Token: 0x040014B2 RID: 5298
		AcllFailed,
		// Token: 0x040014B3 RID: 5299
		StoreMountInitiated,
		// Token: 0x040014B4 RID: 5300
		StoreMountSuccessful,
		// Token: 0x040014B5 RID: 5301
		StoreMountFailed,
		// Token: 0x040014B6 RID: 5302
		StoreDismountInitiated,
		// Token: 0x040014B7 RID: 5303
		StoreDismountSuccessful,
		// Token: 0x040014B8 RID: 5304
		StoreDismountFailed,
		// Token: 0x040014B9 RID: 5305
		UpdateMasterServerInitiated,
		// Token: 0x040014BA RID: 5306
		UpdateMasterServerFinished
	}
}
