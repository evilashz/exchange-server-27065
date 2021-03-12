using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000386 RID: 902
	[LocDescription(Strings.IDs.UninstallFfoPrimingServiceTask)]
	[Cmdlet("Uninstall", "FfoPrimingService")]
	public sealed class UninstallFfoPrimingService : ManageFfoPrimingService
	{
		// Token: 0x06001F61 RID: 8033 RVA: 0x00087A28 File Offset: 0x00085C28
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
