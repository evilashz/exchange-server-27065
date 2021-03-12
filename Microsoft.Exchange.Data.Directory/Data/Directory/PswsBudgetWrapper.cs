using System;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C6 RID: 2502
	internal class PswsBudgetWrapper : PowerShellBudgetWrapper
	{
		// Token: 0x0600740C RID: 29708 RVA: 0x0017EF70 File Offset: 0x0017D170
		internal PswsBudgetWrapper(PowerShellBudget innerBudget) : base(innerBudget)
		{
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x0017EF7C File Offset: 0x0017D17C
		protected override void StartLocalImpl(string callerInfo, TimeSpan preCharge)
		{
			if (base.LocalCostHandle != null)
			{
				LocalTimeCostHandle localCostHandle = base.LocalCostHandle;
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey>((long)this.GetHashCode(), "[PswsBudgetWrapper.StartLocalImpl] BudgetWrapper of user \"{0}\" is accessed by multi-thread concurrently.", localCostHandle.Budget.Owner);
				base.LocalCostHandle = null;
				localCostHandle.Dispose();
			}
			base.StartLocalImpl(callerInfo, preCharge);
		}
	}
}
