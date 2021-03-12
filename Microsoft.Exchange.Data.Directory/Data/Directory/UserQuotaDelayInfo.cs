using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009E2 RID: 2530
	internal class UserQuotaDelayInfo : DelayInfo
	{
		// Token: 0x06007587 RID: 30087 RVA: 0x00182556 File Offset: 0x00180756
		public UserQuotaDelayInfo(TimeSpan delay, OverBudgetException exception, bool required) : base(delay, required)
		{
			this.OverBudgetException = exception;
		}

		// Token: 0x06007588 RID: 30088 RVA: 0x00182567 File Offset: 0x00180767
		public static UserQuotaDelayInfo CreateInfinite(OverBudgetException exception)
		{
			return new UserQuotaDelayInfo(Budget.IndefiniteDelay, exception, true);
		}

		// Token: 0x17002A13 RID: 10771
		// (get) Token: 0x06007589 RID: 30089 RVA: 0x00182575 File Offset: 0x00180775
		// (set) Token: 0x0600758A RID: 30090 RVA: 0x0018257D File Offset: 0x0018077D
		public OverBudgetException OverBudgetException { get; private set; }
	}
}
