using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A3 RID: 419
	public class ComponentInfoBaseTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x00043049 File Offset: 0x00041249
		public ComponentInfoBaseTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CollectCmdletLogEntries, typeof(CollectCmdletLogEntriesModule));
			base.RegisterModule(TaskModuleKey.Throttling, typeof(ComponentInfoBaseThrottlingModule));
		}
	}
}
