using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A9 RID: 169
	public class SetADPermissionTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002DA4D File Offset: 0x0002BC4D
		public SetADPermissionTaskModuleFactory()
		{
			base.UnregisterModule(TaskModuleKey.AutoReportProgress);
		}
	}
}
