using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallMonitoringCfgDataHandler : InstallDatacenterRoleBaseDataHandler
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00009869 File Offset: 0x00007A69
		public InstallMonitoringCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "MonitoringRole", "Install-MonitoringRole", connection)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000987D File Offset: 0x00007A7D
		public override void UpdatePreCheckTaskDataHandler()
		{
		}
	}
}
