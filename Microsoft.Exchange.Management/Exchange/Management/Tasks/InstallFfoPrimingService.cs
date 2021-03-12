using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000385 RID: 901
	[LocDescription(Strings.IDs.InstallFfoPrimingServiceTask)]
	[Cmdlet("Install", "FfoPrimingService")]
	public sealed class InstallFfoPrimingService : ManageFfoPrimingService
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x00087A0E File Offset: 0x00085C0E
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
