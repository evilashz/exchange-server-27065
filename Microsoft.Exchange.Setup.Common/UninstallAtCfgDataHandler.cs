using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200005F RID: 95
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallAtCfgDataHandler : UninstallCfgDataHandler
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000FE07 File Offset: 0x0000E007
		public UninstallAtCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000FE12 File Offset: 0x0000E012
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
