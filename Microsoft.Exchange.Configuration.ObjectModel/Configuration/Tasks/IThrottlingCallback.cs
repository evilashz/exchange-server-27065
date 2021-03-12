using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000295 RID: 661
	internal interface IThrottlingCallback
	{
		// Token: 0x060016A1 RID: 5793
		void Initialize();

		// Token: 0x060016A2 RID: 5794
		void CheckResourceHealth(IThrottlingModuleInfo tmInfo);

		// Token: 0x060016A3 RID: 5795
		DelayEnforcementResults EnforceDelay(IThrottlingModuleInfo tmInfo, CostType[] costTypes, TimeSpan cmdletMaxPreferredDelay);
	}
}
