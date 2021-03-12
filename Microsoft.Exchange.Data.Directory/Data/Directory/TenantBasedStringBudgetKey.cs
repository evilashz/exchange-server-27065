using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D5 RID: 2517
	internal class TenantBasedStringBudgetKey : LookupBudgetKey
	{
		// Token: 0x06007481 RID: 29825 RVA: 0x0018035C File Offset: 0x0017E55C
		public TenantBasedStringBudgetKey(string key, OrganizationId organizationId, bool isServiceAccount, BudgetType budgetType) : base(budgetType, isServiceAccount)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			this.Key = key;
			this.organizationId = organizationId;
			this.cachedToString = string.Format("TenantStr~{0}~{1}~{2}~{3}", new object[]
			{
				key,
				organizationId,
				budgetType,
				isServiceAccount
			});
			this.cachedhashCode = this.cachedToString.GetHashCode();
		}

		// Token: 0x17002992 RID: 10642
		// (get) Token: 0x06007482 RID: 29826 RVA: 0x001803E6 File Offset: 0x0017E5E6
		// (set) Token: 0x06007483 RID: 29827 RVA: 0x001803EE File Offset: 0x0017E5EE
		public string Key { get; private set; }

		// Token: 0x17002993 RID: 10643
		// (get) Token: 0x06007484 RID: 29828 RVA: 0x001803F7 File Offset: 0x0017E5F7
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x00180400 File Offset: 0x0017E600
		public override bool Equals(object obj)
		{
			TenantBasedStringBudgetKey tenantBasedStringBudgetKey = obj as TenantBasedStringBudgetKey;
			return !(tenantBasedStringBudgetKey == null) && (tenantBasedStringBudgetKey.BudgetType == base.BudgetType && tenantBasedStringBudgetKey.Key == this.Key && tenantBasedStringBudgetKey.OrganizationId == this.OrganizationId) && tenantBasedStringBudgetKey.IsServiceAccountBudget == base.IsServiceAccountBudget;
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x00180463 File Offset: 0x0017E663
		public override int GetHashCode()
		{
			return this.cachedhashCode;
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x0018046B File Offset: 0x0017E66B
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x00180473 File Offset: 0x0017E673
		internal override IThrottlingPolicy InternalLookup()
		{
			return ThrottlingPolicyCache.Singleton.Get(this.OrganizationId);
		}

		// Token: 0x04004B1C RID: 19228
		private const string ToStringFormat = "TenantStr~{0}~{1}~{2}~{3}";

		// Token: 0x04004B1D RID: 19229
		private readonly OrganizationId organizationId;

		// Token: 0x04004B1E RID: 19230
		private readonly int cachedhashCode;

		// Token: 0x04004B1F RID: 19231
		private readonly string cachedToString;
	}
}
