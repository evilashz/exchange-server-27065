using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000CD RID: 205
	[LocDescription(Strings.IDs.InstallFBAServiceTask)]
	[Cmdlet("Install", "FBAService")]
	public class InstallFBAService : ManageFBAService
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0001ABEE File Offset: 0x00018DEE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
