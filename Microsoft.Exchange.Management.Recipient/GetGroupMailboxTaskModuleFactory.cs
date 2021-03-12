using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200004A RID: 74
	internal sealed class GetGroupMailboxTaskModuleFactory : GetTaskWithIdentityModuleFactory
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00014538 File Offset: 0x00012738
		public GetGroupMailboxTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideTenantDcServerSettingsModule));
		}
	}
}
