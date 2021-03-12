using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisasterRecoveryMntCfgDataHandler : DisasterRecoveryCfgDataHandler
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000062D7 File Offset: 0x000044D7
		public DisasterRecoveryMntCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}
	}
}
