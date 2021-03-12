using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeCentralAdminCfgDataHandler : UpgradeCfgDataHandler
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x000111B6 File Offset: 0x0000F3B6
		public UpgradeCentralAdminCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000111C1 File Offset: 0x0000F3C1
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
