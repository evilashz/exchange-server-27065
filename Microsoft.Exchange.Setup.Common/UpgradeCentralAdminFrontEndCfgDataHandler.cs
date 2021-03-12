using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200006D RID: 109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeCentralAdminFrontEndCfgDataHandler : UpgradeCfgDataHandler
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x000111D0 File Offset: 0x0000F3D0
		public UpgradeCentralAdminFrontEndCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000111DB File Offset: 0x0000F3DB
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
