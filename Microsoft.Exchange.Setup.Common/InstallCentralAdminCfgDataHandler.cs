using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallCentralAdminCfgDataHandler : InstallDatacenterRoleBaseDataHandler
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00007552 File Offset: 0x00005752
		public InstallCentralAdminCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "CentralAdminRole", "Install-CentralAdminRole", connection)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007566 File Offset: 0x00005766
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
