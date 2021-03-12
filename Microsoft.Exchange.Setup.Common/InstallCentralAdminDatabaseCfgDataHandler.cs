using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallCentralAdminDatabaseCfgDataHandler : InstallDatacenterRoleBaseDataHandler
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00007568 File Offset: 0x00005768
		public InstallCentralAdminDatabaseCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "CentralAdminDatabaseRole", "Install-CentralAdminDatabaseRole", connection)
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000757C File Offset: 0x0000577C
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
