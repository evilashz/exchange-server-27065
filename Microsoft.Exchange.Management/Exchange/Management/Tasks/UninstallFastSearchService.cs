using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000750 RID: 1872
	[Cmdlet("Uninstall", "FastSearchService")]
	public class UninstallFastSearchService : ManageSearchService
	{
		// Token: 0x0600425F RID: 16991 RVA: 0x0010FBD6 File Offset: 0x0010DDD6
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
