using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisasterRecoveryAtCfgDataHandler : DisasterRecoveryCfgDataHandler
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00006155 File Offset: 0x00004355
		public DisasterRecoveryAtCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006160 File Offset: 0x00004360
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
