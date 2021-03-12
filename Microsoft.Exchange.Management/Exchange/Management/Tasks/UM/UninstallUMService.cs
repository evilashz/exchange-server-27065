using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D64 RID: 3428
	[Cmdlet("Uninstall", "UMService")]
	[LocDescription(Strings.IDs.UninstallUmServiceTask)]
	public class UninstallUMService : UMServiceTask
	{
		// Token: 0x0600836C RID: 33644 RVA: 0x00218A76 File Offset: 0x00216C76
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
