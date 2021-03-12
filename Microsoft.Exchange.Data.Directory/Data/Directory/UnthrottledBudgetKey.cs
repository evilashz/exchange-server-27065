using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009DF RID: 2527
	internal class UnthrottledBudgetKey : LookupBudgetKey
	{
		// Token: 0x0600750B RID: 29963 RVA: 0x00182114 File Offset: 0x00180314
		public UnthrottledBudgetKey(string id, BudgetType budgetType) : this(id, budgetType, false)
		{
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x00182120 File Offset: 0x00180320
		public UnthrottledBudgetKey(string id, BudgetType budgetType, bool isServiceAccount) : base(budgetType, isServiceAccount)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("Id cannot be null or empty");
			}
			this.Id = id;
			this.cachedHashCode = (base.BudgetType.GetHashCode() ^ this.Id.GetHashCode());
			this.cachedToString = string.Format("Unthrottled~{0}~{1}", id, budgetType);
		}

		// Token: 0x170029A7 RID: 10663
		// (get) Token: 0x0600750D RID: 29965 RVA: 0x00182188 File Offset: 0x00180388
		// (set) Token: 0x0600750E RID: 29966 RVA: 0x00182190 File Offset: 0x00180390
		public string Id { get; private set; }

		// Token: 0x0600750F RID: 29967 RVA: 0x0018219C File Offset: 0x0018039C
		public override bool Equals(object obj)
		{
			UnthrottledBudgetKey unthrottledBudgetKey = obj as UnthrottledBudgetKey;
			return !(unthrottledBudgetKey == null) && unthrottledBudgetKey.BudgetType == base.BudgetType && unthrottledBudgetKey.Id == this.Id;
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x001821DC File Offset: 0x001803DC
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x001821E4 File Offset: 0x001803E4
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x001821EC File Offset: 0x001803EC
		internal override IThrottlingPolicy InternalLookup()
		{
			return UnthrottledThrottlingPolicy.GetSingleton();
		}

		// Token: 0x04004B5E RID: 19294
		private const string ToStringFormat = "Unthrottled~{0}~{1}";

		// Token: 0x04004B5F RID: 19295
		private readonly string cachedToString;

		// Token: 0x04004B60 RID: 19296
		private readonly int cachedHashCode;
	}
}
