using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009E3 RID: 2531
	internal class WSManBudget : PowerShellBudget
	{
		// Token: 0x0600758B RID: 30091 RVA: 0x00182586 File Offset: 0x00180786
		internal WSManBudget(BudgetKey owner, IThrottlingPolicy policy) : base(owner, policy)
		{
		}

		// Token: 0x17002A14 RID: 10772
		// (get) Token: 0x0600758C RID: 30092 RVA: 0x00182590 File Offset: 0x00180790
		protected override bool TrackActiveRunspacePerfCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600758D RID: 30093 RVA: 0x00182593 File Offset: 0x00180793
		protected override void UpdatePolicyValueTakingEffectInThisBudget(SingleComponentThrottlingPolicy policy)
		{
			this.activeRunspacesPolicyValue = policy.MaxConcurrency;
			this.powerShellMaxCmdletsPolicyValue = policy.PowerShellMaxCmdlets;
			this.powerShellMaxCmdletsTimePeriodPolicyValue = policy.PowerShellMaxCmdletsTimePeriod;
			this.powerShellMaxRunspacesPolicyValue = policy.PowerShellMaxRunspaces;
			this.powerShellMaxRunspacesTimePeriodPolicyValue = policy.PowerShellMaxRunspacesTimePeriod;
		}
	}
}
