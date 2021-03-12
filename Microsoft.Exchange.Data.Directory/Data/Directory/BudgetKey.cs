using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A7 RID: 2471
	internal abstract class BudgetKey
	{
		// Token: 0x06007215 RID: 29205 RVA: 0x00179F2E File Offset: 0x0017812E
		public BudgetKey(BudgetType budgetType, bool isServiceAccountBudget)
		{
			this.BudgetType = budgetType;
			this.IsServiceAccountBudget = isServiceAccountBudget;
		}

		// Token: 0x17002849 RID: 10313
		// (get) Token: 0x06007216 RID: 29206 RVA: 0x00179F44 File Offset: 0x00178144
		// (set) Token: 0x06007217 RID: 29207 RVA: 0x00179F4C File Offset: 0x0017814C
		public BudgetType BudgetType { get; private set; }

		// Token: 0x1700284A RID: 10314
		// (get) Token: 0x06007218 RID: 29208 RVA: 0x00179F55 File Offset: 0x00178155
		// (set) Token: 0x06007219 RID: 29209 RVA: 0x00179F5D File Offset: 0x0017815D
		public bool IsServiceAccountBudget { get; private set; }

		// Token: 0x0600721A RID: 29210 RVA: 0x00179F66 File Offset: 0x00178166
		public static bool operator ==(BudgetKey key1, BudgetKey key2)
		{
			return object.Equals(key1, key2);
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x00179F6F File Offset: 0x0017816F
		public static bool operator !=(BudgetKey key1, BudgetKey key2)
		{
			return !object.Equals(key1, key2);
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x00179F7B File Offset: 0x0017817B
		public override bool Equals(object obj)
		{
			throw new NotImplementedException("BudgetKey.Equals must be overridden in derived classes");
		}

		// Token: 0x0600721D RID: 29213 RVA: 0x00179F87 File Offset: 0x00178187
		public override int GetHashCode()
		{
			throw new NotImplementedException("BudgetKey.GetHashCode must be overridden in derived classes.");
		}

		// Token: 0x040049D8 RID: 18904
		public static Func<BudgetKey, IThrottlingPolicy> LookupPolicyForTest;
	}
}
