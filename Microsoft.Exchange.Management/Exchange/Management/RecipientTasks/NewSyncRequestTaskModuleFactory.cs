using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CBE RID: 3262
	public class NewSyncRequestTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x06007D09 RID: 32009 RVA: 0x001FF6DC File Offset: 0x001FD8DC
		public NewSyncRequestTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideUserDcServerSettingsModule));
		}
	}
}
