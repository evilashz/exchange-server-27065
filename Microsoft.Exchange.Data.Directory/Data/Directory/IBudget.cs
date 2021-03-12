using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009AD RID: 2477
	internal interface IBudget : IDisposable
	{
		// Token: 0x06007240 RID: 29248
		void StartLocal(string callerInfo, TimeSpan preCharge = default(TimeSpan));

		// Token: 0x06007241 RID: 29249
		void EndLocal();

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x06007242 RID: 29250
		LocalTimeCostHandle LocalCostHandle { get; }

		// Token: 0x06007243 RID: 29251
		DelayInfo GetDelay();

		// Token: 0x06007244 RID: 29252
		DelayInfo GetDelay(ICollection<CostType> consideredCostTypes);

		// Token: 0x06007245 RID: 29253
		void CheckOverBudget();

		// Token: 0x06007246 RID: 29254
		void CheckOverBudget(ICollection<CostType> consideredCostTypes);

		// Token: 0x06007247 RID: 29255
		bool TryCheckOverBudget(out OverBudgetException exception);

		// Token: 0x06007248 RID: 29256
		bool TryCheckOverBudget(ICollection<CostType> consideredCostTypes, out OverBudgetException exception);

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x06007249 RID: 29257
		BudgetKey Owner { get; }

		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x0600724A RID: 29258
		IThrottlingPolicy ThrottlingPolicy { get; }

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x0600724B RID: 29259
		TimeSpan ResourceWorkAccomplished { get; }

		// Token: 0x0600724C RID: 29260
		void ResetWorkAccomplished();

		// Token: 0x0600724D RID: 29261
		bool TryGetBudgetBalance(out string budgetBalance);
	}
}
