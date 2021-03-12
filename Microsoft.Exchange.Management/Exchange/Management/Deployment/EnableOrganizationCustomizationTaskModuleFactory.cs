using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A7 RID: 423
	public class EnableOrganizationCustomizationTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x000430D2 File Offset: 0x000412D2
		public EnableOrganizationCustomizationTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.Throttling, typeof(EnableOrganizationCustomizationThrottlingModule));
		}
	}
}
