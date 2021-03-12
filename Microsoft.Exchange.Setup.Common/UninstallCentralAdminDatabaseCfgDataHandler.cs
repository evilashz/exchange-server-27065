using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallCentralAdminDatabaseCfgDataHandler : UninstallCfgDataHandler
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000FE21 File Offset: 0x0000E021
		public UninstallCentralAdminDatabaseCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
