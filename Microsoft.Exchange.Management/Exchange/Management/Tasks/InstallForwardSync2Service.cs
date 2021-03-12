using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000382 RID: 898
	[LocDescription(Strings.IDs.InstallForwardSyncServiceTask)]
	[Cmdlet("Install", "ForwardSync2Service")]
	public class InstallForwardSync2Service : ManageForwardSync2Service
	{
		// Token: 0x06001F59 RID: 8025 RVA: 0x00087911 File Offset: 0x00085B11
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
