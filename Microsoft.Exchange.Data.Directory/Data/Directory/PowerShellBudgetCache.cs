using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C2 RID: 2498
	internal class PowerShellBudgetCache : BudgetCache<PowerShellBudget>
	{
		// Token: 0x060073D9 RID: 29657 RVA: 0x0017DA1C File Offset: 0x0017BC1C
		protected override PowerShellBudget CreateBudget(BudgetKey key, IThrottlingPolicy policy)
		{
			BudgetType budgetType = key.BudgetType;
			if (budgetType == BudgetType.PowerShell)
			{
				return new PowerShellBudget(key, policy);
			}
			switch (budgetType)
			{
			case BudgetType.WSMan:
				return new WSManBudget(key, policy);
			case BudgetType.WSManTenant:
				return new WSManTenantBudget(key, policy);
			default:
				throw new ArgumentException("PowerShellBudgetCache can only be used to create Power-ish budgets.  Passed budget key: " + key);
			}
		}

		// Token: 0x04004ACA RID: 19146
		public static readonly PowerShellBudgetCache Singleton = new PowerShellBudgetCache();
	}
}
