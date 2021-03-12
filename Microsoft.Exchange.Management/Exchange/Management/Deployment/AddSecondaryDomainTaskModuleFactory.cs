using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A4 RID: 420
	public class AddSecondaryDomainTaskModuleFactory : ComponentInfoBaseTaskModuleFactory
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x00043074 File Offset: 0x00041274
		public AddSecondaryDomainTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(AddSecondaryDomainHealthCountersModule));
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(AddSecondaryDomainServerSettingsModule));
		}
	}
}
