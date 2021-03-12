using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeMonitoringCfgDataHandler : UpgradeCfgDataHandler
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00011D04 File Offset: 0x0000FF04
		public UpgradeMonitoringCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00011D0F File Offset: 0x0000FF0F
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
