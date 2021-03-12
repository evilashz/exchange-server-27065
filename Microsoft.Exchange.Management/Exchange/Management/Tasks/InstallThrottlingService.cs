using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000CFD RID: 3325
	[Cmdlet("Install", "ThrottlingService")]
	[LocDescription(Strings.IDs.InstallThrottlingServiceTask)]
	public sealed class InstallThrottlingService : ManageThrottlingService
	{
		// Token: 0x06007FDA RID: 32730 RVA: 0x0020AFB8 File Offset: 0x002091B8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
