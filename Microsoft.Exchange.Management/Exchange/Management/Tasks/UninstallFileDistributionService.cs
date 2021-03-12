using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000770 RID: 1904
	[Cmdlet("Uninstall", "FileDistributionService")]
	public class UninstallFileDistributionService : ManageFileDistributionService
	{
		// Token: 0x0600435F RID: 17247 RVA: 0x00114841 File Offset: 0x00112A41
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
