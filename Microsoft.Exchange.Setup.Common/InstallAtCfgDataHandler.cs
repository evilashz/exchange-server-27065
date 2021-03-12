using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallAtCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00007402 File Offset: 0x00005602
		public InstallAtCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "AdminToolsRole", "Install-AdminToolsRole", connection)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007416 File Offset: 0x00005616
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
