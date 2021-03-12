using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000279 RID: 633
	internal class ForestWideTenantDcServerSettingsModule : RunspaceServerSettingsInitModule
	{
		// Token: 0x060015D8 RID: 5592 RVA: 0x00051AF2 File Offset: 0x0004FCF2
		public ForestWideTenantDcServerSettingsModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00051AFB File Offset: 0x0004FCFB
		protected override ADServerSettings GetCmdletADServerSettings()
		{
			return base.CreateADServerSettingsForOrganization(true);
		}
	}
}
