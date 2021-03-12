using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallDatacenterRoleBaseDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00007545 File Offset: 0x00005745
		public InstallDatacenterRoleBaseDataHandler(ISetupContext context, string installableUnitName, string commandText, MonadConnection connection) : base(context, installableUnitName, commandText, connection)
		{
		}
	}
}
