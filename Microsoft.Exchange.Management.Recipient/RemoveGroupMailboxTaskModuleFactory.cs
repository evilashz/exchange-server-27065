using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000053 RID: 83
	internal sealed class RemoveGroupMailboxTaskModuleFactory : ADObjectTaskModuleFactory
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00016728 File Offset: 0x00014928
		public RemoveGroupMailboxTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideTenantDcServerSettingsModule));
		}
	}
}
