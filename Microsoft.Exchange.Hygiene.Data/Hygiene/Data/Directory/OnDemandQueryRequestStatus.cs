using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000EF RID: 239
	internal enum OnDemandQueryRequestStatus
	{
		// Token: 0x040004ED RID: 1261
		NotStarted,
		// Token: 0x040004EE RID: 1262
		InProgress,
		// Token: 0x040004EF RID: 1263
		Success,
		// Token: 0x040004F0 RID: 1264
		UserCancel,
		// Token: 0x040004F1 RID: 1265
		Failed,
		// Token: 0x040004F2 RID: 1266
		Expired,
		// Token: 0x040004F3 RID: 1267
		OverTenantQuota,
		// Token: 0x040004F4 RID: 1268
		OverSystemQuota
	}
}
