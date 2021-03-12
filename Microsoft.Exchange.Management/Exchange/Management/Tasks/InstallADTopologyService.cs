using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002CD RID: 717
	[LocDescription(Strings.IDs.InstallADTopologyServiceTask)]
	[Cmdlet("Install", "ADTopologyService")]
	public class InstallADTopologyService : ManageADTopologyService
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x00070EC0 File Offset: 0x0006F0C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
