using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000045 RID: 69
	internal sealed class NewGroupMailboxTaskModuleFactory : NewMailboxTaskModuleFactory
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000E147 File Offset: 0x0000C347
		public NewGroupMailboxTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideTenantDcServerSettingsModule));
		}
	}
}
