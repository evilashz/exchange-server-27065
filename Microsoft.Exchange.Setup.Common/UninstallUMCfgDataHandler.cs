using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000068 RID: 104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallUMCfgDataHandler : UninstallCfgDataHandler
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x000110D7 File Offset: 0x0000F2D7
		public UninstallUMCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, RoleManager.GetRoleByName("UnifiedMessagingRole"), connection)
		{
		}
	}
}
