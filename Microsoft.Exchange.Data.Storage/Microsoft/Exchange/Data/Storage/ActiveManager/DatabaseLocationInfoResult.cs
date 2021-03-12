using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002FF RID: 767
	public enum DatabaseLocationInfoResult
	{
		// Token: 0x04001441 RID: 5185
		Success,
		// Token: 0x04001442 RID: 5186
		Unknown,
		// Token: 0x04001443 RID: 5187
		InTransitSameSite,
		// Token: 0x04001444 RID: 5188
		InTransitCrossSite,
		// Token: 0x04001445 RID: 5189
		SiteViolation
	}
}
