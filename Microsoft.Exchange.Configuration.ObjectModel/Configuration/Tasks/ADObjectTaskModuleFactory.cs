using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000291 RID: 657
	internal class ADObjectTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x00055912 File Offset: 0x00053B12
		public ADObjectTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.Throttling, typeof(ADResourceThrottlingModule));
		}
	}
}
