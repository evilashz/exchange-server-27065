using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A6 RID: 422
	public class CompleteOrganizationUpgradeTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x000430B8 File Offset: 0x000412B8
		public CompleteOrganizationUpgradeTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(CompleteOrganizationUpgradeHealthCountersModule));
		}
	}
}
