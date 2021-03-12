using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003D9 RID: 985
	internal class EwsBudgetCache : BudgetCache<EwsBudget>
	{
		// Token: 0x06001B96 RID: 7062 RVA: 0x0009CB78 File Offset: 0x0009AD78
		protected override EwsBudget CreateBudget(BudgetKey key, IThrottlingPolicy policy)
		{
			EwsBudget ewsBudget = new EwsBudget(key, policy);
			ewsBudget.CheckOverBudget();
			return ewsBudget;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0009CB94 File Offset: 0x0009AD94
		protected override void AfterCacheHit(BudgetKey key, EwsBudget value)
		{
			base.AfterCacheHit(key, value);
			value.CheckOverBudget();
		}

		// Token: 0x04001221 RID: 4641
		public static readonly EwsBudgetCache Singleton = new EwsBudgetCache();
	}
}
