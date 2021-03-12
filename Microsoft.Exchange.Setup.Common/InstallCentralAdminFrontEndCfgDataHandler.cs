using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallCentralAdminFrontEndCfgDataHandler : InstallDatacenterRoleBaseDataHandler
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000757E File Offset: 0x0000577E
		public InstallCentralAdminFrontEndCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "CentralAdminFrontEndRole", "Install-CentralAdminFrontEndRole", connection)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007592 File Offset: 0x00005792
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
