using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallCentralAdminFrontEndCfgDataHandler : UninstallCfgDataHandler
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000FE2E File Offset: 0x0000E02E
		public UninstallCentralAdminFrontEndCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000FE39 File Offset: 0x0000E039
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
