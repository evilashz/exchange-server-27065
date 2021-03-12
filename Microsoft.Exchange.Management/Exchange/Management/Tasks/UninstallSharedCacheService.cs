using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000773 RID: 1907
	[Cmdlet("Uninstall", "SharedCacheService")]
	public class UninstallSharedCacheService : ManageSharedCacheService
	{
		// Token: 0x06004366 RID: 17254 RVA: 0x00114957 File Offset: 0x00112B57
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
