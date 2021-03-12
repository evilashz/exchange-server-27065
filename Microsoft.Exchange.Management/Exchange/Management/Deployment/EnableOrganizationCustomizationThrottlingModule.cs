using System;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001AE RID: 430
	internal class EnableOrganizationCustomizationThrottlingModule : ComponentInfoBaseThrottlingModule
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x00043188 File Offset: 0x00041388
		public EnableOrganizationCustomizationThrottlingModule(TaskContext context) : base(context)
		{
			this.budgets.Add(new BudgetInformation
			{
				Budget = PowerShellBudget.Acquire(TenantHydrationBudgetKey.Singleton),
				ThrottledEventInfo = TaskEventLogConstants.Tuple_SlimTenantTaskThrottled
			});
		}
	}
}
