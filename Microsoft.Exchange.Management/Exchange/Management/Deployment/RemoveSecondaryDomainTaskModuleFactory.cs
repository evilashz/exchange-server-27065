using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001AC RID: 428
	public class RemoveSecondaryDomainTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x00043164 File Offset: 0x00041364
		public RemoveSecondaryDomainTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(RemoveSecondaryDomainHealthCountersModule));
		}
	}
}
