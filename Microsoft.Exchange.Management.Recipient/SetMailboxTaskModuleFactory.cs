using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200006C RID: 108
	public class SetMailboxTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x0001FB19 File Offset: 0x0001DD19
		public SetMailboxTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideUserDcServerSettingsModule));
		}
	}
}
