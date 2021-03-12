using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008CA RID: 2250
	[Cmdlet("Uninstall", "MSExchangeReplService")]
	[LocDescription(Strings.IDs.UninstallReplayServiceTask)]
	public sealed class UninstallReplService : ManageReplService
	{
		// Token: 0x06004FDF RID: 20447 RVA: 0x0014E820 File Offset: 0x0014CA20
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			base.UninstallEventManifest();
			base.RestoreDynamicPortRange();
			TaskLogger.LogExit();
		}
	}
}
