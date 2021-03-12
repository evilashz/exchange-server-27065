using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000056 RID: 86
	internal sealed class SetGroupMailboxTaskModuleFactory : ADObjectTaskModuleFactory
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00017B62 File Offset: 0x00015D62
		public SetGroupMailboxTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideTenantDcServerSettingsModule));
		}
	}
}
