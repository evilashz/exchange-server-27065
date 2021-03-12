using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D8 RID: 2520
	internal class TenantHydrationBudgetKey : LookupBudgetKey
	{
		// Token: 0x06007495 RID: 29845 RVA: 0x00180639 File Offset: 0x0017E839
		private TenantHydrationBudgetKey() : base(BudgetType.PowerShell, false)
		{
			this.cachedHashCode = "TenantHydrationBudgetKey under FirstOrg".GetHashCode();
		}

		// Token: 0x06007496 RID: 29846 RVA: 0x00180653 File Offset: 0x0017E853
		public override bool Equals(object obj)
		{
			return obj is TenantHydrationBudgetKey;
		}

		// Token: 0x06007497 RID: 29847 RVA: 0x00180660 File Offset: 0x0017E860
		internal override IThrottlingPolicy InternalLookup()
		{
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			string distinguishedName = string.Format("CN={0},CN=Global Settings,{1}", "TenantHydrationThrottlingPolicy", rootOrgContainerIdForLocalForest.DistinguishedName);
			ADObjectId throttlingPolicyId = new ADObjectId(distinguishedName);
			return ThrottlingPolicyCache.Singleton.Get(OrganizationId.ForestWideOrgId, throttlingPolicyId);
		}

		// Token: 0x06007498 RID: 29848 RVA: 0x001806A0 File Offset: 0x0017E8A0
		public override string ToString()
		{
			return "TenantHydrationBudgetKey under FirstOrg";
		}

		// Token: 0x06007499 RID: 29849 RVA: 0x001806A7 File Offset: 0x0017E8A7
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x04004B24 RID: 19236
		private const string CachedToString = "TenantHydrationBudgetKey under FirstOrg";

		// Token: 0x04004B25 RID: 19237
		private readonly int cachedHashCode;

		// Token: 0x04004B26 RID: 19238
		public static readonly TenantHydrationBudgetKey Singleton = new TenantHydrationBudgetKey();
	}
}
