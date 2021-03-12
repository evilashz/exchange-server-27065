using System;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009AF RID: 2479
	internal class CachableThrottlingPolicyItem : CachableItem
	{
		// Token: 0x06007271 RID: 29297 RVA: 0x0017B281 File Offset: 0x00179481
		public CachableThrottlingPolicyItem(IThrottlingPolicy policy)
		{
			this.ThrottlingPolicy = policy;
		}

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x06007272 RID: 29298 RVA: 0x0017B290 File Offset: 0x00179490
		// (set) Token: 0x06007273 RID: 29299 RVA: 0x0017B298 File Offset: 0x00179498
		public IThrottlingPolicy ThrottlingPolicy { get; private set; }

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x06007274 RID: 29300 RVA: 0x0017B2A1 File Offset: 0x001794A1
		public override long ItemSize
		{
			get
			{
				return 1L;
			}
		}
	}
}
