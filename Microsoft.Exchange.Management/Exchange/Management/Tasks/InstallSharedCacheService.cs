using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000772 RID: 1906
	[Cmdlet("Install", "SharedCacheService")]
	public class InstallSharedCacheService : ManageSharedCacheService
	{
		// Token: 0x06004364 RID: 17252 RVA: 0x0011493D File Offset: 0x00112B3D
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
