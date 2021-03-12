using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A5 RID: 421
	public class ManageOrganizationTaskModuleFactory : ComponentInfoBaseTaskModuleFactory
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x0004309F File Offset: 0x0004129F
		public ManageOrganizationTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.Throttling, typeof(ADResourceThrottlingModule));
		}
	}
}
