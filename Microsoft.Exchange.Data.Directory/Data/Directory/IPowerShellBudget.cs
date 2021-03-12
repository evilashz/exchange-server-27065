using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009BD RID: 2493
	internal interface IPowerShellBudget : IBudget, IDisposable
	{
		// Token: 0x060073AC RID: 29612
		CostHandle StartCmdlet(string cmdletName);

		// Token: 0x060073AD RID: 29613
		CostHandle StartActiveRunspace();

		// Token: 0x060073AE RID: 29614
		bool TryCheckOverBudget(CostType costType, out OverBudgetException exception);

		// Token: 0x060073AF RID: 29615
		void CheckOverBudget(CostType costType);

		// Token: 0x17002947 RID: 10567
		// (get) Token: 0x060073B0 RID: 29616
		int TotalActiveRunspacesCount { get; }

		// Token: 0x060073B1 RID: 29617
		void CorrectRunspacesLeak(int leakedValue);

		// Token: 0x060073B2 RID: 29618
		string GetWSManBudgetUsage();

		// Token: 0x060073B3 RID: 29619
		string GetCmdletBudgetUsage();
	}
}
