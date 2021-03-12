using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000417 RID: 1047
	[LocDescription(Strings.IDs.UninstallAssistantsServiceTask)]
	[Cmdlet("Uninstall", "AssistantsService")]
	public class UninstallAssistantsService : ManageAssistantsService
	{
		// Token: 0x0600247A RID: 9338 RVA: 0x00091497 File Offset: 0x0008F697
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
