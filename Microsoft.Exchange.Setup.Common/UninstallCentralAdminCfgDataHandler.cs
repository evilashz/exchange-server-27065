using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000060 RID: 96
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallCentralAdminCfgDataHandler : UninstallCfgDataHandler
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000FE14 File Offset: 0x0000E014
		public UninstallCentralAdminCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000FE1F File Offset: 0x0000E01F
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
