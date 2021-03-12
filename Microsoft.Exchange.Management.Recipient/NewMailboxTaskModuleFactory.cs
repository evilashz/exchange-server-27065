using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000044 RID: 68
	public class NewMailboxTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000E11C File Offset: 0x0000C31C
		public NewMailboxTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(NewMailboxHealthCountersModule));
			base.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(ForestWideUserDcServerSettingsModule));
		}
	}
}
