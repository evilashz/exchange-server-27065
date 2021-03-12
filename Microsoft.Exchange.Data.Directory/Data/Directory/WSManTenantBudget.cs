using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009E4 RID: 2532
	internal class WSManTenantBudget : PowerShellBudget
	{
		// Token: 0x0600758E RID: 30094 RVA: 0x001825D1 File Offset: 0x001807D1
		internal WSManTenantBudget(BudgetKey owner, IThrottlingPolicy policy) : base(owner, policy)
		{
		}

		// Token: 0x17002A15 RID: 10773
		// (get) Token: 0x0600758F RID: 30095 RVA: 0x001825DB File Offset: 0x001807DB
		protected override string MaxConcurrencyOverBudgetReason
		{
			get
			{
				return "MaxTenantConcurrency";
			}
		}

		// Token: 0x17002A16 RID: 10774
		// (get) Token: 0x06007590 RID: 30096 RVA: 0x001825E2 File Offset: 0x001807E2
		protected override string MaxRunspacesTimePeriodOverBudgetReason
		{
			get
			{
				return "MaxTenantRunspaces";
			}
		}

		// Token: 0x17002A17 RID: 10775
		// (get) Token: 0x06007591 RID: 30097 RVA: 0x001825E9 File Offset: 0x001807E9
		protected override bool TrackActiveRunspacePerfCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x001825EC File Offset: 0x001807EC
		protected override void UpdatePolicyValueTakingEffectInThisBudget(SingleComponentThrottlingPolicy policy)
		{
			this.activeRunspacesPolicyValue = policy.PowerShellMaxTenantConcurrency;
			this.powerShellMaxRunspacesPolicyValue = policy.PowerShellMaxTenantRunspaces;
			this.powerShellMaxRunspacesTimePeriodPolicyValue = policy.PowerShellMaxRunspacesTimePeriod;
		}

		// Token: 0x04004B66 RID: 19302
		public const string MaxTenantRunspacesPart = "MaxTenantRunspaces";
	}
}
