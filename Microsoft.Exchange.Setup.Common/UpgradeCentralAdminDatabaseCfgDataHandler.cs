using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeCentralAdminDatabaseCfgDataHandler : UpgradeCfgDataHandler
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x000111C3 File Offset: 0x0000F3C3
		public UpgradeCentralAdminDatabaseCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000111CE File Offset: 0x0000F3CE
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
