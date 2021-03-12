using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000296 RID: 662
	internal class ResourceThrottlingCallback : IThrottlingCallback
	{
		// Token: 0x060016A5 RID: 5797 RVA: 0x000559A0 File Offset: 0x00053BA0
		public void Initialize()
		{
			ResourceLoadDelayInfo.Initialize();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000559A7 File Offset: 0x00053BA7
		public void CheckResourceHealth(IThrottlingModuleInfo tmInfo)
		{
			ResourceLoadDelayInfo.CheckResourceHealth(tmInfo.PSBudget, tmInfo.GetWorkloadSettings(), tmInfo.ResourceKeys);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000559C0 File Offset: 0x00053BC0
		public DelayEnforcementResults EnforceDelay(IThrottlingModuleInfo tmInfo, CostType[] costTypes, TimeSpan cmdletMaxPreferredDelay)
		{
			return ResourceLoadDelayInfo.EnforceDelay(tmInfo.PSBudget, tmInfo.GetWorkloadSettings(), costTypes, tmInfo.ResourceKeys, cmdletMaxPreferredDelay, new Func<DelayInfo, bool>(tmInfo.OnBeforeDelay));
		}
	}
}
