using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001AB RID: 427
	public class StartOrganizationUpgradeTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x0004314A File Offset: 0x0004134A
		public StartOrganizationUpgradeTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(StartOrganizationUpgradeHealthCountersModule));
		}
	}
}
