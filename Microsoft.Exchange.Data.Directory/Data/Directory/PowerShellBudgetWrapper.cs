using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C4 RID: 2500
	internal class PowerShellBudgetWrapper : BudgetWrapper<PowerShellBudget>, IPowerShellBudget, IBudget, IDisposable
	{
		// Token: 0x060073FF RID: 29695 RVA: 0x0017EE11 File Offset: 0x0017D011
		internal PowerShellBudgetWrapper(PowerShellBudget innerBudget) : base(innerBudget)
		{
		}

		// Token: 0x06007400 RID: 29696 RVA: 0x0017EE1C File Offset: 0x0017D01C
		public CostHandle StartCmdlet(string cmdletName)
		{
			CostHandle costHandle = base.GetInnerBudget().StartCmdlet(cmdletName, new Action<CostHandle>(base.HandleCostHandleRelease));
			base.AddAction(costHandle);
			return costHandle;
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x0017EE4C File Offset: 0x0017D04C
		public CostHandle StartActiveRunspace()
		{
			CostHandle costHandle = base.GetInnerBudget().StartActiveRunspace(new Action<CostHandle>(base.HandleCostHandleRelease));
			base.AddAction(costHandle);
			return costHandle;
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x0017EE79 File Offset: 0x0017D079
		public bool TryCheckOverBudget(CostType costType, out OverBudgetException exception)
		{
			return base.GetInnerBudget().TryCheckOverBudget(costType, out exception);
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x0017EE88 File Offset: 0x0017D088
		public void CheckOverBudget(CostType costType)
		{
			OverBudgetException ex;
			if (this.TryCheckOverBudget(costType, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x1700295C RID: 10588
		// (get) Token: 0x06007404 RID: 29700 RVA: 0x0017EEA2 File Offset: 0x0017D0A2
		public int TotalActiveRunspacesCount
		{
			get
			{
				return base.GetInnerBudget().TotalActiveRunspacesCount;
			}
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x0017EEAF File Offset: 0x0017D0AF
		public void CorrectRunspacesLeak(int leakedValue)
		{
			base.GetInnerBudget().CorrectRunspacesLeak(leakedValue);
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x0017EEBD File Offset: 0x0017D0BD
		public string GetWSManBudgetUsage()
		{
			return base.GetInnerBudget().GetWSManBudgetUsage();
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x0017EECA File Offset: 0x0017D0CA
		public string GetCmdletBudgetUsage()
		{
			return base.GetInnerBudget().GetCmdletBudgetUsage();
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x0017EED7 File Offset: 0x0017D0D7
		protected override PowerShellBudget ReacquireBudget()
		{
			return PowerShellBudgetCache.Singleton.Get(base.Owner);
		}
	}
}
