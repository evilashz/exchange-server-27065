using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200027A RID: 634
	internal class ForestWideUserDcServerSettingsModule : RunspaceServerSettingsInitModule
	{
		// Token: 0x060015DA RID: 5594 RVA: 0x00051B04 File Offset: 0x0004FD04
		public ForestWideUserDcServerSettingsModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00051B10 File Offset: 0x0004FD10
		protected override ADServerSettings GetCmdletADServerSettings()
		{
			bool flag = (SwitchParameter)(base.CurrentTaskContext.InvocationInfo.Fields["ForestWideDomainControllerAffinityByExecutingUser"] ?? false);
			if (flag)
			{
				return base.CreateADServerSettingsForUserWithForestWideAffnity();
			}
			return base.GetCmdletADServerSettings();
		}
	}
}
