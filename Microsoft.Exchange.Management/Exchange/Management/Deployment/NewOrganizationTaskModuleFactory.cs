using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A8 RID: 424
	public class NewOrganizationTaskModuleFactory : ManageOrganizationTaskModuleFactory
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x000430EB File Offset: 0x000412EB
		public NewOrganizationTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(NewOrganizationHealthCountersModule));
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(NewOrganizationServerSettingsModule));
		}
	}
}
