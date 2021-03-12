using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000416 RID: 1046
	[Cmdlet("Install", "AssistantsService")]
	[LocDescription(Strings.IDs.InstallAssistantsServiceTask)]
	public class InstallAssistantsService : ManageAssistantsService
	{
		// Token: 0x06002478 RID: 9336 RVA: 0x0009147D File Offset: 0x0008F67D
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
