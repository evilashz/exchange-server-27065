using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D3 RID: 2515
	internal class StandardBudgetWrapper : StandardBudgetWrapperBase<StandardBudget>
	{
		// Token: 0x06007478 RID: 29816 RVA: 0x00180268 File Offset: 0x0017E468
		internal StandardBudgetWrapper(StandardBudget innerBudget) : base(innerBudget)
		{
		}

		// Token: 0x06007479 RID: 29817 RVA: 0x00180271 File Offset: 0x0017E471
		protected override StandardBudget ReacquireBudget()
		{
			return StandardBudgetCache.Singleton.Get(base.Owner);
		}
	}
}
