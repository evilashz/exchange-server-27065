using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D4 RID: 2516
	internal class StringBudgetKey : LookupBudgetKey
	{
		// Token: 0x0600747A RID: 29818 RVA: 0x00180284 File Offset: 0x0017E484
		public StringBudgetKey(string key, bool isServiceAccount, BudgetType budgetType) : base(budgetType, isServiceAccount)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.Key = key;
			this.cachedToString = string.Format("Str~{0}~{1}~{2}", key, budgetType, isServiceAccount);
			this.cachedhashCode = this.cachedToString.GetHashCode();
		}

		// Token: 0x17002991 RID: 10641
		// (get) Token: 0x0600747B RID: 29819 RVA: 0x001802DC File Offset: 0x0017E4DC
		// (set) Token: 0x0600747C RID: 29820 RVA: 0x001802E4 File Offset: 0x0017E4E4
		public string Key { get; private set; }

		// Token: 0x0600747D RID: 29821 RVA: 0x001802F0 File Offset: 0x0017E4F0
		public override bool Equals(object obj)
		{
			StringBudgetKey stringBudgetKey = obj as StringBudgetKey;
			return !(stringBudgetKey == null) && (stringBudgetKey.BudgetType == base.BudgetType && stringBudgetKey.Key == this.Key) && stringBudgetKey.IsServiceAccountBudget == base.IsServiceAccountBudget;
		}

		// Token: 0x0600747E RID: 29822 RVA: 0x00180340 File Offset: 0x0017E540
		public override int GetHashCode()
		{
			return this.cachedhashCode;
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x00180348 File Offset: 0x0017E548
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x00180350 File Offset: 0x0017E550
		internal override IThrottlingPolicy InternalLookup()
		{
			return ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
		}

		// Token: 0x04004B18 RID: 19224
		private const string ToStringFormat = "Str~{0}~{1}~{2}";

		// Token: 0x04004B19 RID: 19225
		private readonly int cachedhashCode;

		// Token: 0x04004B1A RID: 19226
		private readonly string cachedToString;
	}
}
