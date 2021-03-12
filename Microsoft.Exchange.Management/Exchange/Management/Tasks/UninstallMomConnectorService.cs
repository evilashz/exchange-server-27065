using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000DD RID: 221
	[Cmdlet("Uninstall", "MomConnectorService")]
	[LocDescription(Strings.IDs.UninstallMomConnectorServiceTask)]
	public class UninstallMomConnectorService : ManageMomConnectorService
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x0001BDBF File Offset: 0x00019FBF
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
