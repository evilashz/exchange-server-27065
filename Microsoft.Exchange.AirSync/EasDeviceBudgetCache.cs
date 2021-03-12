using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000286 RID: 646
	internal class EasDeviceBudgetCache : BudgetCache<EasDeviceBudget>
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x0008C95C File Offset: 0x0008AB5C
		protected override EasDeviceBudget CreateBudget(BudgetKey key, IThrottlingPolicy policy)
		{
			EasDeviceBudgetKey easDeviceBudgetKey = key as EasDeviceBudgetKey;
			if (easDeviceBudgetKey == null)
			{
				throw new ArgumentException("key must be an EasDeviceBudgetKey", "key");
			}
			return new EasDeviceBudget(easDeviceBudgetKey, policy);
		}

		// Token: 0x04000E98 RID: 3736
		public static readonly EasDeviceBudgetCache Singleton = new EasDeviceBudgetCache();
	}
}
