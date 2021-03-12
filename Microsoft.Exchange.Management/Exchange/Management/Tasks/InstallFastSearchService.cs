using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200074F RID: 1871
	[Cmdlet("Install", "FastSearchService")]
	public class InstallFastSearchService : ManageSearchService
	{
		// Token: 0x0600425D RID: 16989 RVA: 0x0010FBBC File Offset: 0x0010DDBC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
