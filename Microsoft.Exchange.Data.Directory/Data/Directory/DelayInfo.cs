using System;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B6 RID: 2486
	internal class DelayInfo
	{
		// Token: 0x060072AB RID: 29355 RVA: 0x0017B854 File Offset: 0x00179A54
		public static void TraceMicroDelays(IBudget budget, TimeSpan workAccomplished, TimeSpan microDelay)
		{
			StandardBudgetWrapper standardBudgetWrapper = budget as StandardBudgetWrapper;
			if (standardBudgetWrapper != null)
			{
				int num = (int)standardBudgetWrapper.GetInnerBudget().CasTokenBucket.GetBalance();
				ExTraceGlobals.BudgetDelayTracer.TraceDebug(0L, "Budget: '{0}', Balance: {1}, Work Done: {2}, MicroDelay: {3}", new object[]
				{
					budget.Owner,
					num,
					workAccomplished,
					microDelay
				});
			}
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x0017B8BC File Offset: 0x00179ABC
		public DelayInfo(TimeSpan delay, bool required)
		{
			this.Delay = delay;
			this.Required = required;
		}

		// Token: 0x1700286C RID: 10348
		// (get) Token: 0x060072AD RID: 29357 RVA: 0x0017B8D2 File Offset: 0x00179AD2
		// (set) Token: 0x060072AE RID: 29358 RVA: 0x0017B8DA File Offset: 0x00179ADA
		public TimeSpan Delay { get; private set; }

		// Token: 0x1700286D RID: 10349
		// (get) Token: 0x060072AF RID: 29359 RVA: 0x0017B8E3 File Offset: 0x00179AE3
		// (set) Token: 0x060072B0 RID: 29360 RVA: 0x0017B8EB File Offset: 0x00179AEB
		public bool Required { get; private set; }

		// Token: 0x04004A49 RID: 19017
		public static readonly DelayInfo NoDelay = new DelayInfo(TimeSpan.Zero, false);
	}
}
