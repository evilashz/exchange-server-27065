using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A9 RID: 425
	public class RemoveOrganizationTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x00043116 File Offset: 0x00041316
		public RemoveOrganizationTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(RemoveOrgHealthCountersModule));
		}
	}
}
