using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009BF RID: 2495
	internal class LocalTimeCostHandle : CostHandle
	{
		// Token: 0x060073B6 RID: 29622 RVA: 0x0017D5BD File Offset: 0x0017B7BD
		public LocalTimeCostHandle(Budget budget, Action<CostHandle> onRelease, string description, TimeSpan preCharge = default(TimeSpan)) : base(budget, CostType.CAS, onRelease, description, preCharge)
		{
			this.UnaccountedStartTime = base.StartTime;
		}

		// Token: 0x17002948 RID: 10568
		// (get) Token: 0x060073B7 RID: 29623 RVA: 0x0017D5D7 File Offset: 0x0017B7D7
		// (set) Token: 0x060073B8 RID: 29624 RVA: 0x0017D5DF File Offset: 0x0017B7DF
		public DateTime UnaccountedStartTime { get; set; }

		// Token: 0x17002949 RID: 10569
		// (get) Token: 0x060073B9 RID: 29625 RVA: 0x0017D5E8 File Offset: 0x0017B7E8
		public TimeSpan UnaccountedForTime
		{
			get
			{
				return TimeProvider.UtcNow - this.UnaccountedStartTime;
			}
		}

		// Token: 0x1700294A RID: 10570
		// (get) Token: 0x060073BA RID: 29626 RVA: 0x0017D5FA File Offset: 0x0017B7FA
		// (set) Token: 0x060073BB RID: 29627 RVA: 0x0017D602 File Offset: 0x0017B802
		internal bool ReverseBudgetCharge { get; set; }
	}
}
