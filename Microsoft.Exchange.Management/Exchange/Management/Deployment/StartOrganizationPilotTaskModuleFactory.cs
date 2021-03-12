using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001AA RID: 426
	public class StartOrganizationPilotTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x00043130 File Offset: 0x00041330
		public StartOrganizationPilotTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(StartOrganizationPilotHealthCountersModule));
		}
	}
}
