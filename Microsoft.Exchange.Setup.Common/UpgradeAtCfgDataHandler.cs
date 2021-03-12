using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200006A RID: 106
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeAtCfgDataHandler : UpgradeCfgDataHandler
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x000111A9 File Offset: 0x0000F3A9
		public UpgradeAtCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000111B4 File Offset: 0x0000F3B4
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
