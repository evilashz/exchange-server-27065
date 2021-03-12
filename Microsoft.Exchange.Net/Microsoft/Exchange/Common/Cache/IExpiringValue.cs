using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000681 RID: 1665
	internal interface IExpiringValue
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001E2E RID: 7726
		DateTime ExpirationTime { get; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001E2F RID: 7727
		bool Expired { get; }
	}
}
