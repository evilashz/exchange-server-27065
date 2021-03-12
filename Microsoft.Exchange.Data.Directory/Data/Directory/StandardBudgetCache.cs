using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D0 RID: 2512
	internal class StandardBudgetCache : BudgetCache<StandardBudget>
	{
		// Token: 0x06007466 RID: 29798 RVA: 0x0017FE43 File Offset: 0x0017E043
		protected override StandardBudget CreateBudget(BudgetKey key, IThrottlingPolicy policy)
		{
			return new StandardBudget(key, policy);
		}

		// Token: 0x04004B10 RID: 19216
		public static readonly StandardBudgetCache Singleton = new StandardBudgetCache();
	}
}
