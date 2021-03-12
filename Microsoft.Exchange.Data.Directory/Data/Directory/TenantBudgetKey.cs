using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D6 RID: 2518
	internal class TenantBudgetKey : LookupBudgetKey
	{
		// Token: 0x06007489 RID: 29833 RVA: 0x00180485 File Offset: 0x0017E685
		public TenantBudgetKey(OrganizationId organizationId, BudgetType budgetType) : base(budgetType, false)
		{
			this.organizationId = organizationId;
			this.cachedToString = TenantBudgetKey.GetCachedToString(organizationId, budgetType);
			this.cachedHashCode = this.cachedToString.GetHashCode();
		}

		// Token: 0x17002994 RID: 10644
		// (get) Token: 0x0600748A RID: 29834 RVA: 0x001804B4 File Offset: 0x0017E6B4
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x001804BC File Offset: 0x0017E6BC
		public override bool Equals(object obj)
		{
			return this.InternalEquals(obj);
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x001804C5 File Offset: 0x0017E6C5
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x001804CD File Offset: 0x0017E6CD
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x001804D5 File Offset: 0x0017E6D5
		internal override IThrottlingPolicy InternalLookup()
		{
			return this.LookupPolicyByOrganizationId();
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x001804DD File Offset: 0x0017E6DD
		protected virtual IThrottlingPolicy LookupPolicyByOrganizationId()
		{
			return ThrottlingPolicyCache.Singleton.Get(this.OrganizationId);
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x001804F0 File Offset: 0x0017E6F0
		protected virtual bool InternalEquals(object obj)
		{
			TenantBudgetKey tenantBudgetKey = obj as TenantBudgetKey;
			return !(tenantBudgetKey == null) && tenantBudgetKey.BudgetType == base.BudgetType && tenantBudgetKey.OrganizationId == this.OrganizationId;
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x00180530 File Offset: 0x0017E730
		protected static string GetCachedToString(OrganizationId organizationId, BudgetType budgetType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("oid~");
			stringBuilder.Append(organizationId.ToString());
			stringBuilder.Append("~");
			stringBuilder.Append(budgetType.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04004B21 RID: 19233
		private readonly OrganizationId organizationId;

		// Token: 0x04004B22 RID: 19234
		private readonly int cachedHashCode;

		// Token: 0x04004B23 RID: 19235
		private readonly string cachedToString;
	}
}
