using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008AC RID: 2220
	[LocDescription(Strings.IDs.InstallReplayServiceTask)]
	[Cmdlet("Install", "MSExchangeReplService")]
	public class InstallReplService : ManageReplService
	{
		// Token: 0x06004E66 RID: 20070 RVA: 0x0014535F File Offset: 0x0014355F
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InstallEventManifest();
			base.RegisterDefaultLogCopierPort();
			base.RegisterDefaultHighAvailabilityWebServicePort();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
