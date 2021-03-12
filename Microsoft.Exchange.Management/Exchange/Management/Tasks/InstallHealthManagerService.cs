using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000602 RID: 1538
	[Cmdlet("Install", "HealthManagerService")]
	[LocDescription(Strings.IDs.InstallHealthManagerServiceTask)]
	public class InstallHealthManagerService : ManageHealthManagerService
	{
		// Token: 0x060036C7 RID: 14023 RVA: 0x000E30B0 File Offset: 0x000E12B0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			base.RegisterProcessManagerEventLog();
			base.PersistManagedAvailabilityServersUsgSid();
			TaskLogger.LogExit();
		}
	}
}
